using System;
using System.Collections.Generic;
using System.Net;
using Woofy.Core.Infrastructure;
using Woofy.Core.SystemProxies;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine.Expressions
{
    /// <summary>
    /// Used to iterate between multiple pages. Will search the page for a new link using the argument, and when found it will redirect to that new page.
    /// </summary>
    public class VisitExpression : BaseWebExpression
    {
        private readonly IPageParser parser;
        private readonly IAppController appController;

        public VisitExpression(IPageParser parser, IWebClientProxy webClient, IAppLog appLog, IAppController appController)
            : base(appLog, webClient)
        {
            this.parser = parser;
            this.appController = appController;
        }

        public override IEnumerable<object> Invoke(object argument, Context context)
        {
            if (ContentIsEmpty(context))
            {
                InitializeContent(context);

                if (context.PageContent.IsNotNullOrEmpty())
                {
                    yield return context.CurrentAddress;
                }
                else
                {
                    yield break;
                }
            }

            var regex = (string)argument;
            do
            {
                var links = parser.RetrieveLinksFromPage(regex, context.CurrentAddress, context.PageContent, (r, l) => ReportBadRegex(context, r, l));

                ReportLinksFound(links, context);
				if (links.Length == 0)
					yield break;

            	var link = links[0];
                ReportVisitingPage(link, context);

                context.CurrentAddress = link;
                try
                {
                    context.PageContent = webClient.DownloadString(link);
                }
                catch (WebException ex)
                {
                    Warn(context, ex.Message);
                    yield break;
                }
                yield return link;
            }
            while (true);
        }

        private void ReportInfiniteLoop(Context context)
        {
            Warn(context, "unable to continue - infinite loop detected (check previous warning)");
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