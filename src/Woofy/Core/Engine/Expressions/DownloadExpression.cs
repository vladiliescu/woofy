using System;
using System.Collections.Generic;
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

        public DownloadExpression(IAppLog appLog, IPageParser parser, IApplicationController applicationController, IFileDownloader downloader, IComicPath comicPath, IFileProxy file, IWebClientProxy webClient)
            : base(appLog, webClient)
        {
            this.parser = parser;
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
                return null;
            }
            ReportStripsFound(links, context);

            var downloadedFiles = new List<string>();
        	foreach (var link in links)
        	{
				ReportStripDownloading(link, context);

				var downloadPath = comicPath.DownloadPathFor(context.ComicId, link);
                //i add each file to the downloaded files list regardless if it has actually been downloaded or not; 
                //this is because I want other expressions (e.g. write_meta_to_xmp) to be able to access files that have already been downloaded - this is useful in case Woofy crashes after downloading but before embedding the metadata
                downloadedFiles.Add(downloadPath);      
                if (file.Exists(downloadPath))
                {
                    ReportStripAlreadyDownloaded(link, context);
                    continue;
                }

        	    downloader.Download(link, downloadPath);
				ReportStripDownloaded(link, context);

				Sleep(context);
        	}

            context.DownloadedFiles = downloadedFiles.ToArray();

            return null;
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

        private void ReportStripDownloading(Uri link, Context context)
        {
            Log(context, "downloading {0}", link);
        }

        protected override string ExpressionName
        {
            get { return Expressions.Download; }
        }
    }
}