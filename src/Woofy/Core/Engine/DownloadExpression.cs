using System.Collections.Generic;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine
{
    public class DownloadExpression : IExpression
    {
        private readonly IAppLog appLog;
        private readonly IPageParser parser;
        public DownloadExpression(IAppLog appLog, IPageParser parser)
        {
            this.appLog = appLog;
            this.parser = parser;
        }

        public IEnumerable<object> Invoke(object argument, Context context)
        {
            var links = parser.RetrieveLinksFromPage(context.PageContent, (string)argument, context.CurrentAddress);
            appLog.Send("Downloading comic {0}", links[0]);

            return null;
        }
    }
}