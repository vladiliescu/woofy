using System.Collections.Generic;
using Woofy.Core.SystemProxies;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine
{
    public class VisitExpression : IExpression
    {
        private readonly IPageParser parser;
        private readonly IWebClientProxy webClient;
        private readonly IAppLog appLog;

        public VisitExpression(IPageParser parser, IWebClientProxy webClient, IAppLog appLog)
        {
            this.parser = parser;
            this.webClient = webClient;
            this.appLog = appLog;
        }

        public IEnumerable<object> Invoke(object argument, Context context)
        {
            if (string.IsNullOrEmpty(context.PageContent))
            {
                context.PageContent = webClient.DownloadString(context.CurrentAddress);
                yield return context.CurrentAddress;
            }

            var regex = (string)argument;
            do
            {
                appLog.Send("Visiting page {0}", context.CurrentAddress);
                var links = parser.RetrieveLinksFromPage(context.PageContent, regex, context.CurrentAddress);
                appLog.Send("Found {0} links", links.Length);
                if (links.Length == 0)
                {
                    yield break;
                }

                var link = links[0];
                context.CurrentAddress = link;
                context.PageContent = webClient.DownloadString(link);
                yield return link;
            }
            while (true);
            
        }
    }
}