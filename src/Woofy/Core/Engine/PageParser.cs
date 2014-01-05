using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using Mono.System.Web;
using Uri = Mono.System.Uri;

namespace Woofy.Core.Engine
{
	public interface IPageParser
	{
        Uri[] RetrieveLinksFromPage(string regexPattern, Uri currentUri, string pageContent, Action<string, string> reportFormatException);
		string[] RetrieveContent(string regex, string pageContent);
	}

    public class PageParser : IPageParser
    {
		private readonly IAppSettings appSettings;

		public PageParser(IAppSettings appSettings)
		{
			this.appSettings = appSettings;
		}

        public Uri[] RetrieveLinksFromPage(string regex, Uri currentUri, string pageContent, Action<string, string> reportFormatException)
		{
			var rawLinks = RetrieveContent(regex, pageContent);
            if (rawLinks.Length == 0)
                return new Uri[0];

            var baseUri = GetBaseUri(pageContent);
			var links = new List<Uri>();
			foreach (var link in rawLinks)
			{
                try
                {
                    if (Uri.IsWellFormedUriString(link, Mono.System.UriKind.Absolute))
                        links.Add(new Uri(link));
                    else if (baseUri != null)
                        links.Add(new Uri(baseUri, link));
                    else
                        links.Add(new Uri(currentUri, link));
                }
                catch (UriFormatException)
                {
                    reportFormatException(regex, link);
                }
			}

			return links.ToArray();
		}

        private Uri GetBaseUri(string pageContent)
        {
            var baseElement = RetrieveContent(@"<base(.*?)href=""(?<content>.*?)""", pageContent);
            if (baseElement.Length == 0)
                return null;

            Uri baseUri;
            if (!Uri.TryCreate(baseElement[0], Mono.System.UriKind.Absolute, out baseUri))
                return null;

            return baseUri;
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
