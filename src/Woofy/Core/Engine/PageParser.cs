using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using Woofy.Core.ComicManagement;

namespace Woofy.Core.Engine
{
	public interface IPageParser
	{
		Uri[] RetrieveLinksFromPage(string pageContent, string regexPattern, Uri currentUri);
	}

    public class PageParser : IPageParser
    {
		private readonly IAppSettings appSettings;
		public PageParser(IAppSettings appSettings)
		{
			this.appSettings = appSettings;
		}

		public Uri[] RetrieveLinksFromPage(string pageContent, string regexPattern, Uri currentUri)
		{
			var matches = Regex.Matches(pageContent, regexPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);

			var links = new List<Uri>();
			foreach (Match match in matches)
			{
				var capturedContent = match.Groups[appSettings.ContentGroupName].Success ? match.Groups[appSettings.ContentGroupName].Value : match.Value;

				//just in case someone html-encoded the link; happened with Gone With The Blastwave;
				capturedContent = HttpUtility.HtmlDecode(capturedContent);

				if (WebPath.IsAbsolute(capturedContent))
					links.Add(new Uri(capturedContent));
				else
					links.Add(new Uri(currentUri, capturedContent));
			}

			return links.ToArray();
		}


        public Dictionary<string, string> GetCaptures(string pageContent, string urlContent, ComicDefinition definition)
        {
            var captures = new Dictionary<string, string>();

            foreach (var capture in definition.Captures)
            {
				var match = capture.Target == CaptureTarget.Body ?
					Regex.Match(pageContent, capture.Content) :
					Regex.Match(urlContent, capture.Content);

                if (!match.Success)
                    continue;

                string capturedContent;
                if (match.Groups[appSettings.ContentGroupName].Success)
					capturedContent = match.Groups[appSettings.ContentGroupName].Value;
                else
                    capturedContent = match.Value;

                captures.Add(capture.Name, capturedContent);
            }

            return captures;
        }
    }
}
