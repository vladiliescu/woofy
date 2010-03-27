using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;
using System.Web;
using Woofy.Exceptions;

namespace Woofy.Core
{
    public class ComicsProvider
    {
    	private readonly bool randomPausesBetweenRequests;

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
        private readonly IFileDownloader _comicsDownloader;
        private readonly ComicDefinition _comicInfo;
        //private WebClient _client;
        private bool _isDownloadCancelled;
		private Random random = new Random();
        #endregion

        #region Constants
        public const string ContentGroup = "content";
        #endregion

        #region .ctor
        public ComicsProvider(ComicDefinition comicInfo, string downloadFolder, bool randomPausesBetweenRequests)
            : this(comicInfo, new FileDownloader(downloadFolder), randomPausesBetweenRequests)
        {
        	
        }

    	public ComicsProvider(ComicDefinition comicInfo, IFileDownloader comicsDownloader, bool randomPausesBetweenRequests)
        {
            _comicInfo = comicInfo;
            _comicsDownloader = comicsDownloader;
			this.randomPausesBetweenRequests = randomPausesBetweenRequests;

            //_client = WebConnectionFactory.GetNewWebClientInstance();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Downloads the specified number of comic strips.
        /// </summary>
        /// <param name="comicsToDownload">Number of comics to download. Pass <see cref="AllAvailableComics"/> in order to download all the available comics.</param>
        /// <param name="startUrl">Url at which the download should start.</param>
        public DownloadOutcome DownloadComics(int comicsToDownload, string startUrl)
        {
            _isDownloadCancelled = false;
            var downloadOutcome = DownloadOutcome.Successful;

            try
            {
                Logger.Debug("Downloading comic {0}.", _comicInfo.FriendlyName);
                string properStartUrl;
                if (string.IsNullOrEmpty(startUrl) || startUrl.Equals(_comicInfo.StartUrl, StringComparison.OrdinalIgnoreCase))
                    properStartUrl = GetProperStartUrl(startUrl, _comicInfo.LatestPageRegex);
                else
                    properStartUrl = startUrl;

                var rootUri = string.IsNullOrEmpty(_comicInfo.RootUrl) ? null : new Uri(_comicInfo.RootUrl);
                var currentUrl = properStartUrl;

                for (var i = 0; i < comicsToDownload || comicsToDownload == AllAvailableComics; i++)
                {
                    if (_isDownloadCancelled)
                    {
                        Logger.Debug("Download stopped by user.");

                        downloadOutcome = DownloadOutcome.Cancelled;
                        break;
                    }

                    Logger.Debug("Visiting page {0}.", currentUrl);

                    string pageContent;
                    var request = (HttpWebRequest)WebConnectionFactory.GetNewWebRequestInstance(currentUrl);
                    Uri responseUri;
                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        var reader = new StreamReader(response.GetResponseStream());
                        pageContent = reader.ReadToEnd();

                        responseUri = response.ResponseUri;
                    }

                    var comicLinks = RetrieveComicLinksFromPage(pageContent, rootUri ?? responseUri, _comicInfo);
                    var backButtonLink = RetrieveBackButtonLinkFromPage(pageContent, rootUri ?? responseUri, _comicInfo);
                    var captures = new PageParser(pageContent, currentUrl, _comicInfo).GetCaptures();

                    if (!MatchedLinksObeyRules(comicLinks.Length, _comicInfo.AllowMissingStrips, _comicInfo.AllowMultipleStrips, ref downloadOutcome))
                        break;

                    var fileAlreadyDownloaded = false;
                    var backButtonStringLink = backButtonLink == null ? null : backButtonLink.AbsoluteUri;
                    foreach (var comicLink in comicLinks)
                    {
                        var fileName = GetFileName(comicLink.AbsoluteUri, _comicInfo.RenamePattern, captures);
                        if (!string.IsNullOrEmpty(_comicInfo.RenamePattern))
                        {
                            Logger.Debug("Renamed strip to:");
                            Logger.Debug("\t>>{0}", fileName);
                        }

                        _comicsDownloader.DownloadFile(comicLink.AbsoluteUri, currentUrl, fileName, out fileAlreadyDownloaded);

                        if (fileAlreadyDownloaded && comicsToDownload == AllAvailableComics)    //if the file hasn't been downloaded, then all new comics have been downloaded => exit
                            break;

                        OnDownloadComicCompleted(new DownloadStripCompletedEventArgs(i + 1, backButtonStringLink));
                    }

                    //HACK
                    if (fileAlreadyDownloaded && comicsToDownload == AllAvailableComics)    //if the file hasn't been downloaded, then all new comics have been downloaded => exit
                        break;

                    if (backButtonLink == null)
                        break;

                    currentUrl = backButtonLink.AbsoluteUri;

					if (randomPausesBetweenRequests)
						PauseForRandomPeriod();
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
            catch (RegexException ex)
            {
                Logger.Debug("Encountered an exception while searching for regex matches: {0}.", ex.Message);
                downloadOutcome = DownloadOutcome.Error;
            }
            catch (IOException ex)
            {
                //if the network goes down while downloading a strip, the FileDownloader will throw an IOException containing a SocketException
                //yummy
                if (!(ex.InnerException is SocketException))
                    throw;
                Logger.Debug("Could not access the network.");
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

		private void PauseForRandomPeriod()
		{
			Thread.Sleep(500 + random.Next(1500));
		}

        private string GetFileName(string fileLink, string renamePattern, IDictionary<string, string> captures)
        {
            var baseFileName = fileLink.Substring(fileLink.LastIndexOf('/') + 1);
            var fileName = baseFileName;
            if (!string.IsNullOrEmpty(renamePattern))
            {
                fileName = Regex.Replace(renamePattern, @"\$\{(?<name>[\w\d]*)\}",
                    delegate(Match match)
                    {
                        var name = match.Groups["name"].Value;
                        if (name == "fileName")
                            return Path.GetFileNameWithoutExtension(baseFileName);

                        return captures[name];
                    }
                );

                if (!fileName.Contains("."))
                    fileName += Path.GetExtension(baseFileName);
            }

            return fileName;
        }

        public static Uri[] RetrieveLinksFromPage(string pageContent, Uri currentUri, string regex)
        {
            var links = new List<Uri>();
            MatchCollection matches;

            try
            {
                matches = Regex.Matches(pageContent, regex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            }
            catch (ArgumentException ex)
            {
                throw new RegexException(ex.Message, ex);
            }

            foreach (Match match in matches)
            {
                string capturedContent = match.Groups[ContentGroup].Success ? match.Groups[ContentGroup].Value : match.Value;

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
        /// <param name="downloadOutcome">If a rule is not obeyed, then this parameter will contain the respective outcome.</param>
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
            var request = (HttpWebRequest)WebConnectionFactory.GetNewWebRequestInstance(startUrl);
            Uri uri;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var reader = new StreamReader(response.GetResponseStream());
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
        /// <param name="currentUri"></param>
        /// <returns></returns>
        private Uri RetrieveBackButtonLinkFromPage(string pageContent, Uri currentUri, ComicDefinition comicInfo)
        {
            if (comicInfo == null) throw new ArgumentNullException("comicInfo");
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
            
            Logger.Debug("No links match the backButtonRegex element. The comic has completed.");

            return null;
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