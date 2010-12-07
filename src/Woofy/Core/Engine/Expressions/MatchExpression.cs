using System.Collections.Generic;
using Woofy.Core.SystemProxies;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine.Expressions
{
    public class MatchExpression : BaseWebExpression
    {
        private readonly IPageParser parser;

        public MatchExpression(IAppLog appLog, IWebClientProxy webClient, IPageParser parser) : base(appLog, webClient)
        {
            this.parser = parser;
        }

        public override IEnumerable<object> Invoke(object argument, Context context)
        {
            EnsureContentIsInitialized(context);

            var regex = (string)argument;

            var content = parser.RetrieveContent(regex, context.PageContent);
            return new object[] { content.Length != 0 };
        }

        protected override string ExpressionName
        {
            get { return Expressions.Match; }
        }
    }
}