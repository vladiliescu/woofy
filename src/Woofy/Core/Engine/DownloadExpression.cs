using System;
using System.Collections.Generic;
using System.Threading;
using Woofy.Core.Infrastructure;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine
{
    public class DownloadExpression : BaseExpression
    {
        private readonly IPageParser parser;
        private readonly IApplicationController applicationController;
        public DownloadExpression(IAppLog appLog, IPageParser parser, IApplicationController applicationController)
            : base(appLog)
        {
            this.parser = parser;
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

            ReportStripDownloading(context, links);
            Thread.Sleep(500);
            ReportStripDownloaded(context, links);

            return null;
        }

        private void ReportNoStripsFound(Context context)
        {
            Log(context, "No strips found.");
        }

        private void ReportStripDownloaded(Context context, Uri[] links)
        {
            Log(context, "downloaded {0}", links[0]);
            applicationController.Raise(new StripDownloaded(context.ComicId));
        }

        private void ReportStripDownloading(Context context, Uri[] links)
        {
            Log(context, "downloading {0}", links[0]);
        }

        protected override string ExpressionName
        {
            get { return "download"; }
        }
    }
}