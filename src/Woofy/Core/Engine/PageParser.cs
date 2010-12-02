using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace Woofy.Core.Engine
{
	public interface IPageParser
	{
		Uri[] RetrieveLinksFromPage(string regexPattern, Uri currentUri, string pageContent);
		string[] RetrieveContent(string regex, string pageContent);
	}

    public class PageParser : IPageParser
    {
		private readonly IAppSettings appSettings;

		public PageParser(IAppSettings appSettings)
		{
			this.appSettings = appSettings;
		}

		public Uri[] RetrieveLinksFromPage(string regex, Uri currentUri, string pageContent)
		{
			var rawLinks = RetrieveContent(regex, pageContent);
			var links = new List<Uri>();
			foreach (var link in rawLinks)
			{
				if (WebPath.IsAbsolute(link))
					links.Add(new Uri(link));
				else
					links.Add(new Uri(currentUri, link));
			}

			return links.ToArray();
		}

    	public string[] RetrieveContent(string regex, string pageContent)
    	{
			var matches = Regex.Matches(pageContent, regex, RegexOptions.IgnoreCase | RegexOptions.Multiline);

			var content = new List<string>();
			foreach (Match match in matches)
			{
				var capturedContent = match.Groups[appSettings.ContentGroupName].Success ? 
					match.Groups[appSettings.ContentGroupName].Value : 
					match.Value;

				capturedContent = HttpUtility.HtmlDecode(capturedContent);
				content.Add(capturedContent);
			}

			return content.ToArray();
    	}
    }
}
