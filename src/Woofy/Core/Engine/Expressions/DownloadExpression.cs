using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Woofy.Core.Infrastructure;
using Woofy.Core.SystemProxies;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine.Expressions
{
    public class DownloadExpression : BaseWebExpression
    {
        private readonly IPageParser parser;
        private readonly IAppController appController;
		private readonly IFileDownloader downloader;
		private readonly IComicPath comicPath;
        private readonly IFileProxy file;
        private readonly IAppSettings appSettings;

        public DownloadExpression(IAppLog appLog, IPageParser parser, IAppController appController, IFileDownloader downloader, IComicPath comicPath, IFileProxy file, IWebClientProxy webClient, IAppSettings appSettings)
            : base(appLog, webClient)
        {
            this.parser = parser;
            this.appSettings = appSettings;
            this.file = file;
            this.comicPath = comicPath;
			this.downloader = downloader;
        	this.appController = appController;
        }

        public override IEnumerable<object> Invoke(object argument, Context context)
        {
            EnsureContentIsInitialized(context);

            var links = parser.RetrieveLinksFromPage((string)argument, context.CurrentAddress, context.PageContent);
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

                downloader.Download(link, downloadPath);
                downloadedFiles.Add(downloadPath);
                ReportStripDownloaded(link, context);

                EmbedMetadataIfEnabled(downloadPath, context);

                Sleep(context);
            }

            return downloadedFiles.ToArray();
        }

        private void EmbedMetadataIfEnabled(string fileName, Context context)
        {
            var metaBuilder = new StringBuilder();

            metaBuilder.AddIfPossible("xmp:title", "title", context);
            metaBuilder.AddIfPossible("xmp:description", "description", context);

            metaBuilder.AddIfPossible("xmp:source", context.CurrentAddress.AbsoluteUri);
            metaBuilder.AddIfPossible("comment", "downloaded with Woofy - http://code.google.com/p/woofy");
            
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

        private void ReportStripDownloading(Uri link, string downloadPath, Context context)
        {
            Log(context, "downloading {0} to {1}", link, downloadPath);
        }

        private void Sleep(Context context)
    	{
			Log(context, "sleeping for 2 seconds..");
			Thread.Sleep(2000);
    	}

        private void ReportStripsFound(Uri[] links, Context context)
        {
            Log(context, "found {0} strips", links.Length);
        }

        private void ReportStripAlreadyDownloaded(Uri link, Context context)
        {
            Warn(context, "already downloaded {0}.", link);
        }

        private void ReportNoStripsFound(Context context)
        {
            Warn(context, "no strips found.");
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

            AppendFormat(builder, xmpTag, value);
        }

        public static void AppendFormat(StringBuilder builder, string tag, string value)
        {
            builder.AppendFormat(@" -{0}=""{1}""", tag, value);
        }
    }
}