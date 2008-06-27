using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;
using System.Web;

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
        private ComicDefinition _comicInfo;
        //private WebClient _client;
        private bool _isDownloadCancelled;
        #endregion

        #region Constants
        private const string ContentGroup = "content";
        #endregion

        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ComicsProvider"/> class.
        /// </summary>
        /// <param name="comicInfo">An instance of the <see cref="ComicDefinition"/> class, used to determine how to get the comic links.</param>
        /// <param name="downloadFolder">The folder to which the comics should be downloaded.</param>
        public ComicsProvider(ComicDefinition comicInfo, string downloadFolder)
            : this(comicInfo, new FileDownloader(downloadFolder))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComicsProvider"/> class.
        /// </summary>
        /// <param name="comicInfo">An instance of the <see cref="ComicInfo"/> class, used to determine how to get the comic links.</param>
        public ComicsProvider(ComicDefinition comicInfo, IFileDownloader comicsDownloader)
        {
            _comicInfo = comicInfo;
            _comicsDownloader = comicsDownloader;

            //_client = WebConnectionFactory.GetNewWebClientInstance();
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
                Logger.Debug("Downloading comic {0}.", _comicInfo.FriendlyName);
                string properStartUrl;
                if (startUrl.Equals(_comicInfo.StartUrl, StringComparison.OrdinalIgnoreCase))
                    properStartUrl = GetProperStartUrl(startUrl, _comicInfo.LatestPageRegex);
                else
                    properStartUrl = startUrl;

                Uri rootUri = string.IsNullOrEmpty(_comicInfo.RootUrl) ? null : new Uri(_comicInfo.RootUrl);
                string currentUrl = properStartUrl;

                for (int i = 0; i < comicsToDownload || comicsToDownload == AllAvailableComics; i++)
                {
                    if (_isDownloadCancelled)
                    {
                        Logger.Debug("Download stopped by user.");

                        downloadOutcome = DownloadOutcome.Cancelled;
                        break;
                    }

                    Logger.Debug("Visiting page {0}.", currentUrl);

                    string pageContent;
                    HttpWebRequest request = (HttpWebRequest)WebConnectionFactory.GetNewWebRequestInstance(currentUrl);
                    Uri responseUri;
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        pageContent = reader.ReadToEnd();

                        responseUri = response.ResponseUri;
                    }


                    Uri[] comicLinks = RetrieveComicLinksFromPage(pageContent, rootUri == null ? responseUri : rootUri, _comicInfo);
                    Uri backButtonLink = RetrieveBackButtonLinkFromPage(pageContent, rootUri == null ? responseUri : rootUri, _comicInfo);

                    if (!MatchedLinksObeyRules(comicLinks.Length, _comicInfo.AllowMissingStrips, _comicInfo.AllowMultipleStrips, ref downloadOutcome))
                        break;

                    bool fileAlreadyDownloaded = false;
                    string backButtonStringLink = backButtonLink == null ? null : backButtonLink.AbsoluteUri;
                    foreach (Uri comicLink in comicLinks)
                    {
                        
                        _comicsDownloader.DownloadFile(comicLink.AbsoluteUri, currentUrl, out fileAlreadyDownloaded);

                        if (fileAlreadyDownloaded && comicsToDownload == ComicsProvider.AllAvailableComics)    //if the file hasn't been downloaded, then all new comics have been downloaded => exit
                            break;

                        OnDownloadComicCompleted(new DownloadStripCompletedEventArgs(i + 1, backButtonStringLink));
                    }

                    //HACK
                    if (fileAlreadyDownloaded && comicsToDownload == ComicsProvider.AllAvailableComics)    //if the file hasn't been downloaded, then all new comics have been downloaded => exit
                        break;

                    if (backButtonLink == null)
                        break;

                    currentUrl = backButtonLink.AbsoluteUri;
                }
            }
            catch (UriFormatException ex)
            {
                Logger.Debug("Encountered an exception while downloading: {0}.", ex.Message);

                downloadOutcome = DownloadOutcome.Error;
            }
            catch (WebException ex)
            {
                Logger.Debug("Encountered an exception while downloading: {0}.", ex.Message);

                downloadOutcome = DownloadOutcome.Error;
            }

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
        public static Uri[] RetrieveLinksFromPage(string pageContent, Uri currentUri, string regex)
        {
            List<Uri> links = new List<Uri>();
            MatchCollection matches = Regex.Matches(pageContent, regex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);

            foreach (Match match in matches)
            {
                string capturedContent;
                if (match.Groups[ContentGroup].Success)
                    capturedContent = match.Groups[ContentGroup].Value;
                else
                    capturedContent = match.Value;

                //just in case someone html-encoded the link; happened with Gone With The Blastwave;
                capturedContent = HttpUtility.HtmlDecode(capturedContent);

                if (WebPath.IsAbsolute(capturedContent))
                    links.Add(new Uri(capturedContent));
                else
                    links.Add(new Uri(currentUri, capturedContent));
            }

            return links.ToArray();
        }

        /// <summary>
        /// Checks if the matched comic links obey the comic's download rules (i.e. no multiple strip matches).
        /// </summary>
        /// <param name="comicLinks">The list of matched comic links.</param>
        /// <param name="downloadOutcome">If a rule is not obeyed, then this parameter will contain the respective outcome.</param>
        /// <returns>True if the matched links obey the rules, false otherwise.</returns>
        public static bool MatchedLinksObeyRules(int linksLength, bool allowMissingStrips, bool allowMultipleStrips, ref DownloadOutcome downloadOutcome)
        {
            if (linksLength == 0 && !allowMissingStrips)
            {
                downloadOutcome = DownloadOutcome.NoStripMatchesRuleBroken;
                return false;
            }

            if (linksLength > 1 && !allowMultipleStrips)
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

            Logger.Debug("Visiting page {0}.", startUrl);
            string pageContent;
            HttpWebRequest request = (HttpWebRequest)WebConnectionFactory.GetNewWebRequestInstance(startUrl);
            Uri uri;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                pageContent = reader.ReadToEnd();

                uri = response.ResponseUri;
            }

            return GetProperStartUrlFromPage(pageContent, uri/*startUrl*/, latestPageRegex);
        }

        public static string GetProperStartUrlFromPage(string pageContent, Uri pageUri, string latestPageRegex)
        {
            if (string.IsNullOrEmpty(latestPageRegex))
                return pageUri.AbsoluteUri;

            Uri[] links = RetrieveLinksFromPage(pageContent, pageUri, latestPageRegex);

            if (links.Length == 0)
            {
                Logger.Debug("No links match the latestPageRegex element. Using the startUrl element ({0}).", pageUri);
                return pageUri.AbsoluteUri;
            }

            bool isFirst = true;
            Logger.Debug("Found {0} link(s) that match the latestPageRegex element:", links.Length);
            foreach (Uri link in links)
            {
                if (isFirst)
                {
                    Logger.Debug("\t>>{0}.", link);
                    isFirst = false;
                }
                else
                {
                    Logger.Debug("\t{0}.", link);
                }
            }

            return links[0].AbsoluteUri;
        }

        /// <summary>
        /// Returns the back button link from the specified page, or, if there are several back button links in the page, it returns null.
        /// </summary>
        /// <param name="pageContent">Page content.</param>
        /// <returns></returns>
        private Uri RetrieveBackButtonLinkFromPage(string pageContent, Uri currentUri, ComicDefinition comicInfo)
        {
            Uri[] backButtonLinks = RetrieveLinksFromPage(pageContent, currentUri, comicInfo.BackButtonRegex);

            if (backButtonLinks.Length > 0)
            {
                bool isFirst = true;
                Logger.Debug("Found {0} link(s):", backButtonLinks.Length);
                foreach (Uri link in backButtonLinks)
                {
                    if (isFirst)
                    {
                        Logger.Debug("\t>>{0}.", link);
                        isFirst = false;
                    }
                    else
                    {
                        Logger.Debug("\t{0}.", link);
                    }
                }

                return backButtonLinks[0];
            }
            else
            {
                Logger.Debug("No links match the backButtonRegex element. The comic has completed.");

                return null;
            }
        }

        private Uri[] RetrieveComicLinksFromPage(string pageContent, Uri pageUri, ComicDefinition comicInfo)
        {
            Uri[] comicLinks = RetrieveLinksFromPage(pageContent, pageUri, comicInfo.ComicRegex);
            Logger.Debug("Found {0} strip(s):", comicLinks.Length);
            foreach (Uri comicLink in comicLinks)
            {
                Logger.Debug("\t{0}.", comicLink);
            }

            return comicLinks;
        }
        #endregion

        #region DownloadComicCompleted Event

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
            EventHandler<DownloadCompletedEventArgs> eventReference = _downloadCompleted;

            if (eventReference != null)
                eventReference(this, new DownloadCompletedEventArgs(downloadOutcome));
        }
        #endregion
    }
}