using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Woofy.Core
{
    public class PageParser
    {
        public string PageContent { get; private set; }
        public ComicDefinition Definition { get; private set; }

        public PageParser(string pageContent, ComicDefinition definition)
        {
            PageContent = pageContent;
            Definition = definition;
        }

        public Dictionary<string, string> GetCaptures()
        {
            var captures = new Dictionary<string, string>();

            foreach (var capture in Definition.Captures)
            {
                var match = Regex.Match(PageContent, capture.Content);
                if (!match.Success)
                    continue;

                string capturedContent;
                if (match.Groups[ComicsProvider.ContentGroup].Success)
                    capturedContent = match.Groups[ComicsProvider.ContentGroup].Value;
                else
                    capturedContent = match.Value;


                captures.Add(capture.Name, capturedContent);
            }

            return captures;
        }
    }
}
