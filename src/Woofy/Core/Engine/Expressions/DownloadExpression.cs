using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using Woofy.Core.ComicManagement;
using Woofy.Core.Infrastructure;
using Woofy.Core.SystemProxies;
using Woofy.Flows.ApplicationLog;
using System.Linq;

namespace Woofy.Core.Engine.Expressions
{
    /// <summary>
    /// Used to download an item from the current page. If it finds multiple matches in the current page, it will try to download them all;
    /// </summary>
    public class DownloadExpression : BaseWebExpression
    {
        private readonly IPageParser parser;
        private readonly IAppController appController;
		private readonly IFileDownloader downloader;
		private readonly IComicPath comicPath;
        private readonly IFileProxy file;
        private readonly IAppSettings appSettings;
        private readonly IComicStore comicStore;

        public DownloadExpression(IAppLog appLog, IPageParser parser, IAppController appController, IFileDownloader downloader, IComicPath comicPath, IFileProxy file, IWebClientProxy webClient, IAppSettings appSettings, IComicStore comicStore)
            : base(appLog, webClient)
        {
            this.parser = parser;
            this.comicStore = comicStore;
            this.appSettings = appSettings;
            this.file = file;
            this.comicPath = comicPath;
			this.downloader = downloader;
        	this.appController = appController;
        }

        public override IEnumerable<object> Invoke(object argument, Context context)
        {
            if (!TryToEnsureThatContentIsInitialized(context))
            {
                ReportContentEmpty(context);
                return new object[0];
            }

            var arg = (argument is IEnumerable<string>) ? ((IEnumerable<string>)argument).FirstOrDefault() : (string)argument;
            var links = parser.RetrieveLinksFromPage(arg, context.CurrentAddress, context.PageContent, (r, l) => ReportBadRegex(context, r, l));            

            if (links.Length == 0)
            {
                ReportNoStripsFound(context);
                return new object[0];
            }
            ReportStripsFound(links, context);

            var downloadedFiles = Download(links, context);
            return downloadedFiles;
        }
        
        private string[] Download(IEnumerable<Uri> links, Context context)
        {
            var downloadedFiles = new List<string>();
            foreach (var link in links)
            {
                var downloadPath = comicPath.DownloadPathFor(context.ComicId, link);

                ReportStripDownloading(link, downloadPath, context);
                if (
                    file.Exists(comicPath.DownloadPathForPreviousIndex(context.ComicId, link)) ||
                    file.Exists(downloadPath)
                    )
                {
                    ReportStripAlreadyDownloaded(link, context);
                    continue;
                }

                try
                {
                    downloader.Download(link, downloadPath);
                }
                catch (WebException ex)
                {
                    Warn(context, ex.Message);
                    continue;
                }

                downloadedFiles.Add(downloadPath);
                ReportStripDownloaded(link, context);

                EmbedMetadataIfEnabled(downloadPath, context);

                Sleep(context);
            }

            return downloadedFiles.ToArray();
        }

        private void EmbedMetadataIfEnabled(string fileName, Context context)
        {
            if (MetadataEmbeddingIsDisabled(context.ComicId))
                return;

            var metaBuilder = new StringBuilder();

            metaBuilder.AddIfPossible("xmp:title", "title", context);
            metaBuilder.AddIfPossible("xmp:description", "description", context);

            metaBuilder.AddIfPossible("xmp:source", context.CurrentAddress.AbsoluteUri);
            metaBuilder.AddIfPossible("comment", "downloaded with Woofy - https://vladiliescu.net/woofy");
            
            var arguments = @"{0} ""{1}""".FormatTo(metaBuilder.ToString(), fileName);
            Log(context, "running exiftool.exe {0}", arguments);

            var run = new ProcessStartInfo(appSettings.ExifToolPath, arguments) { CreateNoWindow = true, RedirectStandardError = true, RedirectStandardOutput = true, UseShellExecute = false };

            var process = Process.Start(run);

            process.WaitForExit();

            var error = process.StandardError.ReadToEnd().Trim();
            var output = process.StandardOutput.ReadToEnd().Trim();

            if (error.IsNotNullOrEmpty())
                Log(context, "exiftool: {0}", error);
            if (output.IsNotNullOrEmpty())
                Log(context, "exiftool: {0}", output);
        }

        private bool MetadataEmbeddingIsDisabled(string comicId)
        {
            var comic = comicStore.Find(comicId);
            return !comic.EmbedMetadata;
        }

        private void ReportStripDownloading(Uri link, string downloadPath, Context context)
        {
            Log(context, "downloading {0} to {1}", link, downloadPath);
        }

        private void Sleep(Context context)
    	{
			Log(context, "sleeping for 3 seconds...");
			Thread.Sleep(3000);
    	}

        private void ReportStripsFound(Uri[] links, Context context)
        {
            Log(context, "found {0} images", links.Length);
        }

        private void ReportStripAlreadyDownloaded(Uri link, Context context)
        {
            Warn(context, "already downloaded {0}.", link);
        }

        private void ReportNoStripsFound(Context context)
        {
            Warn(context, "no images found.");
        }

        private void ReportStripDownloaded(Uri link, Context context)
        {
            Log(context, "downloaded {0}", link);
            appController.Raise(new StripDownloaded(context.ComicId));
        }


        protected override string ExpressionName
        {
            get { return Expressions.Download; }
        }
    }

    static class StringBuilderXmpExtensions
    {
        public static void AddIfPossible(this StringBuilder builder, string xmpTag, string metadata, Context context)
        {
            if (!context.Metadata.ContainsKey(metadata))
                return;

            AppendFormat(builder, xmpTag, context.Metadata[metadata]);
        }

        public static void AddIfPossible(this StringBuilder builder, string xmpTag, string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            AppendFormat(builder, xmpTag, value.Replace("\"", "\\\""));
        }

        public static void AppendFormat(StringBuilder builder, string tag, string value)
        {
            builder.AppendFormat(@" -{0}=""{1}""", tag, value);
        }
    }
}