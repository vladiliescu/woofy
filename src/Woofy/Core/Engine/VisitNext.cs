using System.Net;

namespace Woofy.Core.Engine
{
    public class VisitNext : IExpression
    {
        private readonly WebClient webClient;

        public string Regex { get; set; }

        public VisitNext()
        {
            webClient = new WebClient();
        }

        public object Execute(Context context)
        {
            var links = Bot.RetrieveLinksFromPage(context.PageContent, context.CurrentAddress, Regex);
            if (links.Length == 0)
                return null;

            var pageContent = webClient.DownloadString(links[0]);
            context.CurrentAddress = links[0];
            context.PageContent = pageContent;

            return true;
        }
    }
}