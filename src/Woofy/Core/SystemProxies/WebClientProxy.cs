using System;
using System.Net;

namespace Woofy.Core.SystemProxies
{
	public interface IWebClientProxy
	{
		string DownloadString(Uri address);
	}

	public class WebClientProxy : IWebClientProxy
	{
		readonly WebClient webClient = new WebClient();

		public string DownloadString(Uri address)
		{
			return webClient.DownloadString(address);
		}
	}
}