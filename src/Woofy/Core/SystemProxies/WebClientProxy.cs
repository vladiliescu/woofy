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
		readonly CookieAwareWebClient webClient = new CookieAwareWebClient();

	    private readonly IAppLog appLog;

	    public WebClientProxy(IAppLog appLog)
	    {
	        this.appLog = appLog;
	    }

	    public string DownloadString(Uri address)
		{
		    try
		    {
		        return webClient.DownloadString(address);
		    }
		    catch (WebException ex)
		    {
                appLog.Send("WARNING: An error has occurred when downloading {0}", address);
		        LogManager.GetLogger("webClient").Error(ex);

		        return string.Empty;
		    }
		}

		public void Download(Uri address, string fileName)
		{
            try
            {
			    webClient.DownloadFile(address, fileName);
            }
		    catch (WebException ex)
		    {
                appLog.Send("WARNING: An error has occurred when downloading {0}", address);
                LogManager.GetLogger("webClient").Error(ex);
		    }
		}
	}
}