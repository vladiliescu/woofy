using System;
using System.Collections.Generic;
using Woofy.Core.Infrastructure;
using Woofy.Core.SystemProxies;
using Woofy.Flows.ApplicationLog;
using Uri = Mono.System.Uri;

namespace Woofy.Core.Engine.Expressions
{
    /// <summary>
    /// Will redirect to the specified page (not a regular expression, but a valid url)
    /// </summary>
    public class GoToExpression : BaseExpression
    {
        private readonly IAppController appController;
        private readonly IWebClientProxy webClient;

        public GoToExpression(IAppLog appLog, IWebClientProxy webClient, IAppController appController) : base(appLog)
        {
            this.appController = appController;
            this.webClient = webClient;
        }

        public override IEnumerable<object> Invoke(object argument, Context context)
        {
            Uri link = UriEx.From((string)argument, () => ReportBadArgument(context, argument));
            if (link == null)
                return null;            

            ReportVisitingPage(link, context);

            context.CurrentAddress = link;
            context.PageContent = webClient.DownloadString(link);
            
            return null;
        }

        private void ReportBadArgument(Context context, object argument)
        {
            Log(context, "argument {0} is not a valid url", argument);
        }

        private void ReportVisitingPage(Uri page, Context context)
        {
            Log(context, "{0}", page);
            appController.Raise(new CurrentPageChanged(context.ComicId, page));
        }

        protected override string ExpressionName
        {
            get { return Expressions.GoTo; }
        }
    }   
}