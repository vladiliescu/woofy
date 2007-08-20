using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;

using Woofy.Properties;

namespace Woofy.Core
{
    public class ComicsProvider
    {
        #region Public Properties
        /// <summary>
        /// Causes the <see cref="ComicsProvider"/> to get all available comics, instead of a fixed number.
        /// </summary>
        public static int AllAvailableComics
        {
            get { return -1; }
        }
        #endregion

        #region Instance Members
        private IFileDownloader _comicsDownloader;
        private ComicInfo _comicInfo;
        private WebClient _client;
        private bool _isDownloadCancelled;
        private int _comicsToDownload;
        private int _comicsDownloaded;
        private string _backButtonLink;
        private string _url;
        #endregion

        #region Constants
        private const string ContentGroup = "content";
        #endregion

        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ComicsProvider"/> class.
        /// </summary>
        /// <param name="comicInfo">An instance of the <see cref="ComicInfo"/> class, used to determine how to get the comic links.</param>
        /// <param name="downloadFolder">The folder to which the comics should be downloaded.</param>
        public ComicsProvider(ComicInfo comicInfo, string downloadFolder)
            : this(comicInfo, new FileDownloader(downloadFolder))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComicsProvider"/> class.
        /// </summary>
        /// <param name="comicInfo">An instance of the <see cref="ComicInfo"/> class, used to determine how to get the comic links.</param>
        public ComicsProvider(ComicInfo comicInfo, IFileDownloader comicsDownloader)
        {
            _comicInfo = comicInfo;
            _comicsDownloader = comicsDownloader;

            _client = WebConnectionFactory.GetNewWebClientInstance();
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Downloads the specified number of comic strips.
        /// </summary>
        /// <param name="comicsToDownload">Number of comics to download. Pass <see cref="AllAvailableComics"/> in order to download all the available comics.</param>
        public DownloadOutcome DownloadComics(int comicsToDownload)
        {
            return DownloadComics(comicsToDownload, _comicInfo.StartUrl);
        }

        /// <summary>
        /// Downloads the specified number of comic strips.
        /// </summary>
        /// <param name="comicsToDownload">Number of comics to download. Pass <see cref="AllAvailableComics"/> in order to download all the available comics.</param>
        /// <param name="startUrl">Url at which the download should start.</param>
        public DownloadOutcome DownloadComics(int comicsToDownload, string startUrl)
        {
            _isDownloadCancelled = false;
            DownloadOutcome downloadOutcome = DownloadOutcome.Successful;

            try
            {
                string properStartUrl = GetProperStartUrl(startUrl, _comicInfo.LatestPageRegex);
                string currentUrl = properStartUrl;
                bool fileAlreadyDownloaded = false;
                string pageContent;
                string[] comicLinks;
                string backButtonLink;

                for (int i = 0; i < comicsToDownload || comicsToDownload == ComicsProvider.AllAvailableComics; i++)
                {
                    if (_isDownloadCancelled)
                    {
                        downloadOutcome = DownloadOutcome.Cancelled;
                        break;
                    }

                    pageContent = _client.DownloadString(currentUrl);

                    comicLinks = RetrieveLinksFromPage(pageContent, properStartUrl, _comicInfo.ComicRegex);
                    backButtonLink = RetrieveBackButtonLinkFromPage(pageContent, properStartUrl, _comicInfo);

                    if (!MatchedLinksObeyRules(comicLinks, _comicInfo.AllowMissingStrips, _comicInfo.AllowMultipleStrips, ref downloadOutcome))
                        break;

                    foreach (string comicLink in comicLinks)
                    {
                        _comicsDownloader.DownloadFile(comicLink, currentUrl, out fileAlreadyDownloaded);
                    }                    

                    if (fileAlreadyDownloaded && comicsToDownload == ComicsProvider.AllAvailableComics)    //if the file hasn't been downloaded, then all new comics have been downloaded => exit
                        break;

                    OnDownloadComicCompleted(new DownloadStripCompletedEventArgs(i + 1, backButtonLink));

                    if (string.IsNullOrEmpty(backButtonLink))
                        break;

                    currentUrl = backButtonLink;
                }
            }
            catch (UriFormatException) { downloadOutcome = DownloadOutcome.Error; }
            catch (WebException) { downloadOutcome = DownloadOutcome.Error; }

            OnDownloadCompleted(downloadOutcome);

            return downloadOutcome;
        }            

        /// <summary>
        /// Downloads the specified number of comic strips asynchronously.
        /// </summary>
        /// <param name="comicsToDownload">Number of comics to download. Pass <see cref="AllAvailableComics"/> in order to download all the available comics.</param>
        public void DownloadComicsAsync(int comicsToDownload)
        {
            DownloadComicsAsync(comicsToDownload, _comicInfo.StartUrl);
        }

        public void DownloadComicsAsync(int comicsToDownload, string startUrl)
        {
            ThreadPool.UnsafeQueueUserWorkItem(
                delegate
                {
                    DownloadComics(comicsToDownload, startUrl);
                }, null
            );
        }

        /// <summary>
        /// Stops the current download.
        /// </summary>
        public void StopDownload()
        {
            _isDownloadCancelled = true;
        }
        #endregion

        #region Helper Methods
        public static string[] RetrieveLinksFromPage(string pageContent, string currentUrl, string regex)
        {
            List<string> links = new List<string>();
            string currentUrlDirectory = WebPath.GetDirectory(currentUrl);
            string capturedContent;
            MatchCollection matches = Regex.Matches(pageContent, regex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);

            foreach (Match match in matches)
            {
                if (match.Groups[ContentGroup].Success)
                    capturedContent = match.Groups[ContentGroup].Value;
                else
                    capturedContent = match.Value;

                if (WebPath.IsAbsolute(capturedContent))
                    links.Add(capturedContent);
                else
                    links.Add(WebPath.Combine(currentUrlDirectory, capturedContent));
            }

            return links.ToArray();
        }

        /// <summary>
        /// Checks if the matched comic links obey the comic's download rules (i.e. no multiple strip matches).
        /// </summary>
        /// <param name="comicLinks">The list of matched comic links.</param>
        /// <param name="downloadOutcome">If a rule is not obeyed, then this parameter will contain the respective outcome.</param>
        /// <returns>True if the matched links obey the rules, false otherwise.</returns>
        public static bool MatchedLinksObeyRules(string[] comicLinks, bool allowMissingStrips, bool allowMultipleStrips, ref DownloadOutcome downloadOutcome)
        {
            if (comicLinks.Length == 0 && !allowMissingStrips)
            {
                downloadOutcome = DownloadOutcome.NoStripMatchesRuleBroken;
                return false;
            }

            if (comicLinks.Length > 1 && !allowMultipleStrips)
            {
                downloadOutcome = DownloadOutcome.MultipleStripMatchesRuleBroken;
                return false;
            }

            return true;
        }

        private string GetProperStartUrl(string startUrl, string latestPageRegex)
        {
            if (string.IsNullOrEmpty(latestPageRegex))
                return startUrl;

            string pageContent = _client.DownloadString(startUrl);

            return GetProperStartUrlFromPage(pageContent, startUrl, latestPageRegex);
        }

        public static string GetProperStartUrlFromPage(string pageContent, string url, string latestPageRegex)
        {
            if (string.IsNullOrEmpty(latestPageRegex))
                return url;

            string[] links = RetrieveLinksFromPage(pageContent, url, latestPageRegex);

            if (links.Length == 0)
                return url;

            return links[0];
        }

        /// <summary>
        /// Returns the back button link from the specified page, or, if there are several back button links in the page, it returns null.
        /// </summary>
        /// <param name="pageContent">Page content.</param>
        /// <returns></returns>
        private string RetrieveBackButtonLinkFromPage(string pageContent, string currentUrl, ComicInfo comicInfo)
        {
            string[] backButtonLinks = RetrieveLinksFromPage(pageContent, currentUrl, comicInfo.BackButtonRegex);

            if (backButtonLinks.Length > 0)
                return backButtonLinks[0];
            else
                return null;
        }

        #endregion

        #region DownloadComicCompleted Event
        private object _downloadComicCompletedLock = new object();
        private event EventHandler<DownloadStripCompletedEventArgs> _downloadComicCompleted;
        /// <summary>
        /// Occurs when a single comic is downloaded.
        /// </summary>
        public event EventHandler<DownloadStripCompletedEventArgs> DownloadComicCompleted
        {
            add
            {
                _downloadComicCompleted += value;
            }
            remove
            {
                _downloadComicCompleted -= value;
            }
        }

        protected virtual void OnDownloadComicCompleted(DownloadStripCompletedEventArgs e)
        {
            EventHandler<DownloadStripCompletedEventArgs> eventReference = _downloadComicCompleted;

            if (eventReference != null)
                eventReference(this, e);
        }
        #endregion

        #region DownloadCompleted Event
        private event EventHandler<DownloadCompletedEventArgs> _downloadCompleted;
        /// <summary>
        /// Occurs when the entire download is completed.
        /// </summary>
        public event EventHandler<DownloadCompletedEventArgs> DownloadCompleted
        {
            add
            {
                _downloadCompleted += value;
            }
            remove
            {
                _downloadCompleted -= value;
            }
        }

        protected virtual void OnDownloadCompleted(DownloadOutcome downloadOutcome)
        {
            _client.Dispose();

            EventHandler<DownloadCompletedEventArgs> eventReference = _downloadCompleted;

            if (eventReference != null)
                eventReference(this, new DownloadCompletedEventArgs(downloadOutcome));
        }
        #endregion
    }
}