using System;
using System.Net;

namespace Woofy.Core.SystemProxies
{
    public class CookieAwareWebClient : WebClient
    {
        private readonly CookieContainer container = new CookieContainer();

        public CookieAwareWebClient(IAppInfo appInfo)
        {
            Headers.Add("user-agent", string.Format("Woofy/{0}", appInfo.Version.ToPrettyString()));
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            var webRequest = request as HttpWebRequest;
            if (webRequest == null)
                return request;
            
            webRequest.CookieContainer = container;
            return webRequest;
        }
    }
}