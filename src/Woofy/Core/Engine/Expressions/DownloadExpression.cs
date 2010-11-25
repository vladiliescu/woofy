using System;
using System.Collections.Generic;
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
            ReportStripsFound(context, links);

        	foreach (var link in links)
        	{
				ReportStripDownloading(context, link);

				var fileName = parser.RetrieveFileName(link);
				var downloadPath = pathRepository.DownloadPathFor(context.ComicId, fileName);
                if (file.Exists(downloadPath))
                {
                    ReportStripAlreadyDownloaded(context, link);
                    continue;
                }

        	    downloader.Download(link, downloadPath);
				ReportStripDownloaded(context, link);
        	}

            return null;
        }

        private void ReportStripsFound(Context context, Uri[] links)
        {
            Log(context, "found {0} strips", links.Length);
        }

        private void ReportStripAlreadyDownloaded(Context context, Uri link)
        {
            Log(context, "WARNING: already downloaded {0}.", link);
        }

        private void ReportNoStripsFound(Context context)
        {
            Log(context, "WARNING: no strips found.");
        }

        private void ReportStripDownloaded(Context context, Uri link)
        {
            Log(context, "downloaded {0}", link);
            applicationController.Raise(new StripDownloaded(context.ComicId));
        }

        private void ReportStripDownloading(Context context, Uri link)
        {
            Log(context, "downloading {0}", link);
        }

        protected override string ExpressionName
        {
            get { return "download"; }
        }
    }
}