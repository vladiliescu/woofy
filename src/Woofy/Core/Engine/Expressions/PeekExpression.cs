using System;
using System.Collections.Generic;
using Woofy.Core.Infrastructure;
using Woofy.Core.SystemProxies;
using Woofy.Flows.ApplicationLog;
using Uri = Mono.System.Uri;

namespace Woofy.Core.Engine.Expressions
{
    /// <summary>
    /// Used to visit multiple pages found in the current page, one by one.
    /// </summary>
    public class PeekExpression : BaseWebExpression
    {
        private readonly IPageParser parser;
        private readonly IAppController appController;

        public PeekExpression(IAppLog appLog, IWebClientProxy webClient, IPageParser parser, IAppController appController) : base(appLog, webClient)
        {
            this.parser = parser;
            this.appController = appController;
        }

        public override IEnumerable<object> Invoke(object argument, Context context)
        {
            if (ContentIsEmpty(context))
                InitializeContent(context);

            var regex = (string)argument;
            var links = parser.RetrieveLinksFromPage(regex, context.CurrentAddress, context.PageContent, (r, l) => ReportBadRegex(context, r, l));
            ReportLinksFound(links, context);
            if (links.Length == 0)
                yield break;

            foreach (var link in links)
            {
                ReportVisitingPage(link, context);

                context.CurrentAddress = link;
                context.PageContent = webClient.DownloadString(link);
                yield return link;
            }
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
            get { return Expressions.Peek; }
        }
    }
}