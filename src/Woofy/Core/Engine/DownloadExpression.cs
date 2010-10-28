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
                ReportNoComicsFound(context);
                return null;
            }

            ReportComicDownloading(context, links);
            ReportComicDownloaded(context, links);

            return null;
        }

        private void ReportNoComicsFound(Context context)
        {
            Log(context, "No comics found.");
        }

        private void ReportComicDownloaded(Context context, Uri[] links)
        {
            Log(context, "downloaded {0}", links[0]);
            //applicationController.Execute(
        }

        private void ReportComicDownloading(Context context, Uri[] links)
        {
            Log(context, "{0}", links[0]);
        }

        protected override string ExpressionName
        {
            get { return "download"; }
        }
    }
}