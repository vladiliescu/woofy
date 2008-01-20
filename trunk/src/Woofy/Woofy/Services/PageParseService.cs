using System;
using System.Collections.Generic;
using System.Text;

using Woofy.Entities;
using System.Net;
using Woofy.Other;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace Woofy.Services
{
    public class PageParseService
    {
        public Uri RetrieveFaviconUrlFromPage(Uri url)
        {
            Uri defaultFaviconUri = new Uri(url, "favicon.ico");
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(defaultFaviconUri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.ContentLength > 0)
                return defaultFaviconUri;

            string pageContent = string.Empty;
            try
            {
                pageContent = ReadPageContent(url);
            }
            catch (WebException)
            {
                return null;
            }

            Uri[] links = RetrieveLinksFromPageByRegex(Constants.FaviconRegex, pageContent, url);

            if (links.Length > 0)
                return links[0];
            else 
                return null;
        }        

        public Uri[] RetrieveLinksFromPageByRegex(string regex, string pageContent, Uri currentUri)
        {
            List<Uri> links = new List<Uri>();
            MatchCollection matches = Regex.Matches(pageContent, regex, Constants.RegexOptions);

            foreach (Match match in matches)
            {
                string capturedContent;
                if (match.Groups[Constants.ContentGroup].Success)
                    capturedContent = match.Groups[Constants.ContentGroup].Value;
                else
                    capturedContent = match.Value;

                //just in case someone html-encoded the link; happened with Gone With The Blastwave;
                capturedContent = HttpUtility.HtmlDecode(capturedContent);

                Uri newUri;
                if (Uri.TryCreate(capturedContent, UriKind.Absolute, out newUri))
                    links.Add(newUri);
                else 
                    links.Add(new Uri(currentUri, capturedContent));
            }

            return links.ToArray();
        }

        #region Private Methods
        public virtual string ReadPageContent(Uri url)
        {
            WebRequest request = WebConnectionFactory.CreateWebRequest(url);
            string pageContent;
            //Uri currentUri;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());

                pageContent = reader.ReadToEnd();
                //currentUri = response.ResponseUri;
            }

            return pageContent;
        } 
        #endregion
    }
}
