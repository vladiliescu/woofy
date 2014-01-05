using System;
using System.Net;
using Uri = Mono.System.Uri;

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

		public string DownloadString(Uri address)
		{
			return webClient.DownloadString(address);
		}

		public void Download(Uri address, string fileName)
		{
			webClient.DownloadFile(address, fileName);
		}
	}

	public class CookieAwareWebClient : WebClient
	{
		private readonly CookieContainer container = new CookieContainer();

		protected override WebRequest GetWebRequest(Uri address)
		{
			var request = base.GetWebRequest(address);
			if (request is HttpWebRequest)
			{
				(request as HttpWebRequest).CookieContainer = container;
			}
			return request;
		}
	}
}