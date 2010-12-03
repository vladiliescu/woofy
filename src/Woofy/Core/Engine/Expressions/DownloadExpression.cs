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
        private readonly IApplicationController applicationController;
		private readonly IFileDownloader downloader;
		private readonly IComicPath comicPath;
        private readonly IFileProxy file;
        private readonly IAppSettings appSettings;

        public DownloadExpression(IAppLog appLog, IPageParser parser, IApplicationController applicationController, IFileDownloader downloader, IComicPath comicPath, IFileProxy file, IWebClientProxy webClient, IAppSettings appSettings)
            : base(appLog, webClient)
        {
            this.parser = parser;
            this.appSettings = appSettings;
            this.file = file;
            this.comicPath = comicPath;
			this.downloader = downloader;
        	this.applicationController = applicationController;
        }

        public override IEnumerable<object> Invoke(object argument, Context context)
        {
            if (ContentIsEmpty(context))
                InitializeContent(context);

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
                if (file.Exists(downloadPath))
                {
                    ReportStripAlreadyDownloaded(link, context);
                    continue;
                }

                downloader.Download(link, downloadPath);
                downloadedFiles.Add(downloadPath);
                EmbedMetadataIfEnabled(downloadPath, context);

                ReportStripDownloaded(link, context);
                Sleep(context);
            }

            return downloadedFiles.ToArray();
        }

        private void EmbedMetadataIfEnabled(string fileName, Context context)
        {
            var metaBuilder = new StringBuilder();
            AddIfPossible("title", metaBuilder, context);
            AddIfPossible("description", metaBuilder, context);
            AddIfPossible("source", metaBuilder, context.CurrentAddress.AbsoluteUri);
            
            var arguments = @"{0} ""{1}""".FormatTo(metaBuilder.ToString(), fileName);
            Log(context, "running exiftool with the following arguments: {0}", arguments);

            var run = new ProcessStartInfo(appSettings.ExifToolPath, arguments) { CreateNoWindow = true, RedirectStandardOutput = true, UseShellExecute = false };

            var process = Process.Start(run);
            process.WaitForExit();
            Log(context, process.StandardOutput.ReadToEnd());
        }

        private void AddIfPossible(string tag, StringBuilder metaBuilder, Context context)
        {
            if (!context.Metadata.ContainsKey(tag))
                return;

            metaBuilder.AppendFormat(@" -xmp:{0}=""{1}""", tag, context.Metadata[tag]);
        }

        private void AddIfPossible(string tag, StringBuilder metaBuilder, string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            metaBuilder.AppendFormat(@" -xmp:{0}=""{1}""", tag, value);
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
            applicationController.Raise(new StripDownloaded(context.ComicId));
        }

        protected override string ExpressionName
        {
            get { return Expressions.Download; }
        }
    }
}