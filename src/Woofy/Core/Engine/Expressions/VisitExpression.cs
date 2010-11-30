using System;
using System.Collections.Generic;
using Woofy.Core.Infrastructure;
using Woofy.Core.SystemProxies;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine.Expressions
{
    public class VisitExpression : BaseWebExpression
    {
        private readonly IPageParser parser;
        private readonly IWebClientProxy webClient;
        private readonly IApplicationController appController;

        public VisitExpression(IPageParser parser, IWebClientProxy webClient, IAppLog appLog, IApplicationController appController)
            : base(appLog, webClient)
        {
            this.parser = parser;
            this.appController = appController;
            this.webClient = webClient;
        }

        public override IEnumerable<object> Invoke(object argument, Context context)
        {
            if (ContentIsEmpty(context))
            {
                InitializeContent(context);
                yield return context.CurrentAddress;
            }

            var regex = (string)argument;
            do
            {
                var links = parser.RetrieveLinksFromPage(regex, context.CurrentAddress, context.PageContent);
                ReportLinksFound(links, context);
				if (links.Length == 0)
					yield break;

            	var link = links[0];
                ReportVisitingPage(link, context);

                context.CurrentAddress = link;
                context.PageContent = webClient.DownloadString(link);
                yield return link;
            }
            while (true);
        }

        private void ReportLinksFound(Uri[] links, Context context)
        {
            Log(context, "found {0} links", links.Length);
        }

        private void ReportVisitingPage(Uri page, Context context)
        {
            Log(context, "{0}", page);
            appController.Raise(new CurrentPageChanged(context.ComicId, page));
        }

        protected override string ExpressionName
        {
            get { return Expressions.Visit; }
        }
    }
}