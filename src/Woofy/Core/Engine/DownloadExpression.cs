using System;
using System.Collections.Generic;
using Woofy.Core.Infrastructure;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine
{
    public class DownloadExpression : BaseExpression
    {
        private readonly IPageParser parser;
        private readonly IApplicationController applicationController;
		private readonly IFileDownloader downloader;
		private readonly IPathRepository pathRepository;

		public DownloadExpression(IAppLog appLog, IPageParser parser, IApplicationController applicationController, IFileDownloader downloader, IPathRepository pathRepository)
            : base(appLog)
        {
            this.parser = parser;
			this.pathRepository = pathRepository;
			this.downloader = downloader;
        	this.applicationController = applicationController;
        }

        public override IEnumerable<object> Invoke(object argument, Context context)
        {
            var links = parser.RetrieveLinksFromPage(context.PageContent, (string)argument, context.CurrentAddress);
            if (links.Length == 0)
            {
                ReportNoStripsFound(context);
                return null;
            }

        	foreach (var link in links)
        	{
				ReportStripDownloading(context, link);

				var fileName = parser.RetrieveFileName(link);
				var downloadPath = pathRepository.DownloadPathFor(context.ComicId, fileName);
				downloader.Download(link, downloadPath);
				
				ReportStripDownloaded(context, link);
        	}

            return null;
        }

        private void ReportNoStripsFound(Context context)
        {
            Log(context, "No strips found.");
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