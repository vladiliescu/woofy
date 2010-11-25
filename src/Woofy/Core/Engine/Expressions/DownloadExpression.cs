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
		private readonly IPathRepository pathRepository;
        private readonly IFileProxy file;

		public DownloadExpression(IAppLog appLog, IPageParser parser, IApplicationController applicationController, IFileDownloader downloader, IPathRepository pathRepository, IFileProxy file, IWebClientProxy webClient)
            : base(appLog, webClient)
        {
            this.parser = parser;
		    this.file = file;
		    this.pathRepository = pathRepository;
			this.downloader = downloader;
        	this.applicationController = applicationController;
        }

        public override IEnumerable<object> Invoke(object argument, Context context)
        {
            if (ContentIsEmpty(context))
                InitializeContent(context);

            var links = parser.RetrieveLinksFromPage(context.PageContent, (string)argument, context.CurrentAddress);
            if (links.Length == 0)
            {
                ReportNoStripsFound(context);
                return null;
            }
            ReportStripsFound(links, context);

        	foreach (var link in links)
        	{
				ReportStripDownloading(link, context);

				var fileName = parser.RetrieveFileName(link);
				var downloadPath = pathRepository.DownloadPathFor(context.ComicId, fileName);
                if (file.Exists(downloadPath))
                {
                    ReportStripAlreadyDownloaded(link, context);
                    continue;
                }

        	    downloader.Download(link, downloadPath);
				ReportStripDownloaded(link, context);

				Sleep(context);
        	}

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
            Log(context, "WARNING: already downloaded {0}.", link);
        }

        private void ReportNoStripsFound(Context context)
        {
            Log(context, "WARNING: no strips found.");
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
            get { return "download"; }
        }
    }
}