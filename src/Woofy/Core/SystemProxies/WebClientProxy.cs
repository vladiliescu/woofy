using System;
using System.Net;
using NLog;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.SystemProxies
{
	public interface IWebClientProxy
	{
		string DownloadString(Uri address);
		void Download(Uri address, string fileName);
	}

    public class WebClientProxy : IWebClientProxy
	{
		private readonly CookieAwareWebClient webClient;
	    private readonly IAppLog appLog;        

	    public WebClientProxy(IAppLog appLog, IAppInfo appInfo)
	    {
	        this.appLog = appLog;
            webClient = new CookieAwareWebClient(appInfo);
	    }

	    public string DownloadString(Uri address)
		{
		    return webClient.DownloadString(address);
		}

		public void Download(Uri address, string fileName)
		{
			webClient.DownloadFile(address, fileName);
		}
	}
}