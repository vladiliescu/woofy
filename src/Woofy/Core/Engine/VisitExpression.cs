using System;
using System.Collections.Generic;
using Woofy.Core.SystemProxies;

namespace Woofy.Core.Engine
{
    public class VisitExpression : IExpression
    {
        private readonly IPageParser parser;
        private readonly IWebClientProxy webClient;

        public VisitExpression(IPageParser parser, IWebClientProxy webClient)
        {
            this.parser = parser;
            this.webClient = webClient;
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
                var links = parser.RetrieveLinksFromPage(context.PageContent, regex, context.CurrentAddress);
                if (links.Length == 0)
                    yield break;

                context.CurrentAddress = links[0];
                context.PageContent = webClient.DownloadString(links[0]);
                yield return links[0];
            }
            while (true);
        }
    }
}