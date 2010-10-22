using System;
using System.Net;

namespace Woofy.Core.Engine
{
	public class StartAt : IStatement
	{
		private readonly WebClient webClient;

	    public string Url { get; set; }

	    public StartAt()
		{
            webClient = new WebClient();
		}

		public void Execute(Context context)
		{
			var pageContent = webClient.DownloadString(Url);
			context.CurrentAddress = new Uri(Url);
			context.PageContent = pageContent;
		}
	}
}