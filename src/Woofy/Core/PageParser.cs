using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Woofy.Core
{
    public class PageParser
    {
    	private readonly string pageContent;
    	private readonly string urlContent;
    	private readonly ComicDefinition definition;

    	public PageParser(string pageContent, string urlContent, ComicDefinition definition)
        {
            this.pageContent = pageContent;
        	this.urlContent = urlContent;
        	this.definition = definition;
        }

        public Dictionary<string, string> GetCaptures()
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
                if (match.Groups[Bot.ContentGroup].Success)
                    capturedContent = match.Groups[Bot.ContentGroup].Value;
                else
                    capturedContent = match.Value;

                captures.Add(capture.Name, capturedContent);
            }

            return captures;
        }
    }
}
