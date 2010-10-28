using System;
using System.Collections.Generic;
using Woofy.Core.SystemProxies;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine
{
    public class VisitExpression : BaseExpression
    {
        private readonly IPageParser parser;
        private readonly IWebClientProxy webClient;

        public VisitExpression(IPageParser parser, IWebClientProxy webClient, IAppLog appLog)
            : base(appLog)
        {
            this.parser = parser;
            this.webClient = webClient;
        }

        public override IEnumerable<object> Invoke(object argument, Context context)
        {
            if (string.IsNullOrEmpty(context.PageContent))
            {
                Log(context, "Visiting {0}", context.CurrentAddress);
                context.PageContent = webClient.DownloadString(context.CurrentAddress);
                yield return context.CurrentAddress;
            }

            var regex = (string)argument;
            do
            {
                var links = parser.RetrieveLinksFromPage(context.PageContent, regex, context.CurrentAddress);
                Log(context, "Found {0} links", links.Length);
                if (links.Length == 0)
                {
                    yield break;
                }

                var link = links[0];
                Log(context, "Visiting {0}", context.CurrentAddress);

                context.CurrentAddress = link;
                context.PageContent = webClient.DownloadString(link);
                yield return link;
            }
            while (true);
        }

        protected override string ExpressionName
        {
            get { return "visit"; }
        }
    }
}