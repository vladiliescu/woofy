using Woofy.Core.SystemProxies;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine.Expressions
{
    public abstract class BaseWebExpression : BaseExpression
    {
        protected readonly IWebClientProxy webClient;

        protected BaseWebExpression(IAppLog appLog, IWebClientProxy webClient) : base(appLog)
        {
            this.webClient = webClient;
        }

        protected bool ContentIsEmpty(Context context)
        {
            return string.IsNullOrEmpty(context.PageContent);
        }

        protected void InitializeContent(Context context)
        {
            Log(context, "starting at {0}", context.CurrentAddress);

            context.PageContent = webClient.DownloadString(context.CurrentAddress);
        }

        protected void EnsureContentIsInitialized(Context context)
        {
            if (ContentIsEmpty(context))
                InitializeContent(context);
        }

        protected void ReportBadRegex(Context context, string regex, string link)
        {
            Warn(context, "found '{0}' using the regex '{1}', but it's not a valid link", link, regex);
        }
    }
}