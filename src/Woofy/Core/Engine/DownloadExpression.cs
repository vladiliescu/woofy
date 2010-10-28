using System;
using System.Collections.Generic;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine
{
    public class DownloadExpression : BaseExpression
    {
        private readonly IPageParser parser;
        public DownloadExpression(IAppLog appLog, IPageParser parser)
            : base(appLog)
        {
            this.parser = parser;
        }

        public override IEnumerable<object> Invoke(object argument, Context context)
        {
            var links = parser.RetrieveLinksFromPage(context.PageContent, (string)argument, context.CurrentAddress);
            Log(context, "Downloading comic {0}", links[0]);

            return null;
        }

        protected override string ExpressionName
        {
            get { return "download"; }
        }
    }
}