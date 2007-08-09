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
            _client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadPageCompletedCallback);

            _comicsDownloader.DownloadFileCompleted += new EventHandler<DownloadFileCompletedEventArgs>(DownloadComicCompletedCallback);
            _comicsDownloader.DownloadedFileChunk += new EventHandler<DownloadedFileChunkEventArgs>(DownloadedComicChunkCallback);
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Downloads the specified number of comic strips.
        /// </summary>
        /// <param name="comicsToDownload">Number of comics to download. Pass <see cref="AllAvailableComics"/> in order to download all the available comics.</param>
        public void DownloadComics(int comicsToDownload)
        {
            DownloadComics(comicsToDownload, _comicInfo.StartUrl);
        }

        /// <summary>
        /// Downloads the specified number of comic strips.
        /// </summary>
        /// <param name="comicsToDownload">Number of comics to download. Pass <see cref="AllAvailableComics"/> in order to download all the available comics.</param>
        /// <param name="startUrl">Url at which the download should start.</param>
        public void DownloadComics(int comicsToDownload, string startUrl)
        {
            _isDownloadCancelled = false;

            string currentUrl = startUrl;
            bool fileAlreadyDownloaded;
            for (int i = 0; i < comicsToDownload || comicsToDownload == ComicsProvider.AllAvailableComics; i++)
            {
                if (_isDownloadCancelled)
                    return;

                string pageContent = _client.DownloadString(currentUrl);

                string comicLink = RetrieveComicLinkFromPage(pageContent, _comicInfo);
                string backButtonLink = RetrieveBackButtonLinkFromPage(pageContent, _comicInfo);

                if (string.IsNullOrEmpty(comicLink))
                    break;

                _comicsDownloader.DownloadFile(comicLink, out fileAlreadyDownloaded);

                OnDownloadComicCompleted(new DownloadSingleComicCompletedEventArgs(i + 1, currentUrl));

                if (fileAlreadyDownloaded && comicsToDownload == ComicsProvider.AllAvailableComics)    //if the file hasn't been downloaded, then all new comics have been downloaded => exit
                    break;
                if (string.IsNullOrEmpty(backButtonLink))
                    break;

                currentUrl = backButtonLink;
            }

            OnDownloadCompleted();
        }

        /// <summary>
        /// Downloads the specified number of comic strips asynchronously.
        /// </summary>
        /// <param name="comicsToDownload">Number of comics to download. Pass <see cref="AllAvailableComics"/> in order to download all the available comics.</param>
        public void DownloadComicsAsync(int comicsToDownload)
        {
            DownloadComicsAsync(comicsToDownload, _comicInfo.StartUrl);
        }

        /// <summary>
        /// Downloads the specified number of comic strips asynchronously.
        /// </summary>
        /// <param name="comicsToDownload">Number of comics to download. Pass <see cref="AllAvailableComics"/> in order to download all the available comics.</param>
        /// <param name="startUrl">Url at which the download should start.</param>
        public void DownloadComicsAsync(int comicsToDownload, string startUrl)
        {
            _isDownloadCancelled = false;

            if (comicsToDownload != ComicsProvider.AllAvailableComics && comicsToDownload <= 0)
                throw new ArgumentOutOfRangeException("comicsToDownload", "Number of comics to download must be greater than zero or equal to ComicsProvider.AllAvailableComics.");

            if (string.IsNullOrEmpty(startUrl))
                throw new ArgumentNullException("startUrl");

            ThreadPool.UnsafeQueueUserWorkItem(
                delegate
                {
                    DownloadComicsAsyncInternal(comicsToDownload, startUrl);
                }, null);
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
        /// <summary>
        /// Returns the comic link from the specified page, or, if there are several comic links in the page, it returns null.
        /// </summary>
        /// <param name="pageContent">Page content.</param>
        /// <returns></returns>
        public string RetrieveComicLinkFromPage(string pageContent, ComicInfo comicInfo)
        {
            string[] comicLinks = RetrieveComicLinksFromPage(pageContent, comicInfo);

            if (comicLinks.Length > 0)
                return comicLinks[0];
            else
                return null;
        }

        /// <summary>
        /// Returns all comic links found in the specified page.
        /// </summary>
        /// <param name="pageContent">Page content.</param>
        /// <returns></returns>
        public string[] RetrieveComicLinksFromPage(string pageContent, ComicInfo comicInfo)
        {
            List<string> comicLinks = new List<string>();
            MatchCollection comicMatches = Regex.Matches(pageContent, _comicInfo.ComicRegex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);

            foreach (Match comicMatch in comicMatches)
            {
                if (comicMatch.Groups[Settings.Default.ContentGroupName].Success)
                    comicLinks.Add(comicMatch.Groups[Settings.Default.ContentGroupName].Value);
                else if (comicMatch.Groups[Settings.Default.ContentToBeMerged].Success)
                    comicLinks.Add(
                         WebPath.Combine(WebPath.GetDirectory(comicInfo.StartUrl), comicMatch.Groups[Settings.Default.ContentToBeMerged].Value)
                        );
                else
                    comicLinks.Add(comicMatch.Value);
            }

            return comicLinks.ToArray();
        }

        /// <summary>
        /// Returns the back button link from the specified page, or, if there are several back button links in the page, it returns null.
        /// </summary>
        /// <param name="pageContent">Page content.</param>
        /// <returns></returns>
        private string RetrieveBackButtonLinkFromPage(string pageContent, ComicInfo comicInfo)
        {
            string[] backButtonLinks = RetrieveBackButtonLinksFromPage(pageContent, comicInfo);

            if (backButtonLinks.Length > 0)
                return backButtonLinks[0];
            else
                return null;
        }

        /// <summary>
        /// Returns all back button links found in the specified page.
        /// </summary>
        /// <param name="pageContent">Page content.</param>
        /// <returns></returns>
        public string[] RetrieveBackButtonLinksFromPage(string pageContent, ComicInfo comicInfo)
        {
            List<string> backButtonLinks = new List<string>();
            MatchCollection backButtonMatches = Regex.Matches(pageContent, _comicInfo.BackButtonRegex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);

            foreach (Match backButtonMatch in backButtonMatches)
            {
                if (backButtonMatch.Groups[Settings.Default.ContentGroupName].Success)
                    backButtonLinks.Add(backButtonMatch.Groups[Settings.Default.ContentGroupName].Value);
                else if (backButtonMatch.Groups[Settings.Default.ContentToBeMerged].Success)
                    backButtonLinks.Add(
                        WebPath.Combine(WebPath.GetDirectory(comicInfo.StartUrl), backButtonMatch.Groups[Settings.Default.ContentToBeMerged].Value)
                        );
                else
                    backButtonLinks.Add(backButtonMatch.Value);
            }

            return backButtonLinks.ToArray();
        }

        /// <summary>
        /// Does the actual job of asynchronously downloading the comic strips.
        /// </summary>
        /// <param name="comicsToDownload">Number of comics to download. Pass <see cref="AllAvailableComics"/> in order to download all the available comics.</param>
        /// <param name="startUrl">Url at which the download should start.</param>
        private void DownloadComicsAsyncInternal(int comicsToDownload, string startUrl)
        {
            _comicsToDownload = comicsToDownload;
            _comicsDownloaded = 0;

            if (_isDownloadCancelled)
                return;

            _url = startUrl;
            _client.DownloadStringAsync(new Uri(startUrl));
        }
        #endregion

        #region Callbacks
        /// <summary>
        /// Called when a page has been downloaded in async mode.
        /// </summary>
        private void DownloadPageCompletedCallback(object sender, DownloadStringCompletedEventArgs e)
        {
            string pageContent = e.Result;

            string comicLink = RetrieveComicLinkFromPage(pageContent, _comicInfo);
            _backButtonLink = RetrieveBackButtonLinkFromPage(pageContent, _comicInfo);

            if (string.IsNullOrEmpty(comicLink))
            {
                OnDownloadCompleted();
                return;
            }

            if (_isDownloadCancelled)
                return;

            _comicsDownloader.DownloadFileAsync(comicLink, _url);
        }

        /// <summary>
        /// Called when a comic has been downloaded in async mode.
        /// </summary>
        private void DownloadComicCompletedCallback(object sender, DownloadFileCompletedEventArgs e)
        {
            _comicsDownloaded++;
            string currentUrl = _backButtonLink;

            if (e.FileAlreadyDownloaded && _comicsToDownload == ComicsProvider.AllAvailableComics)    //if the file hasn't been downloaded, then all new comics have been downloaded => exit
            {
                OnDownloadCompleted();
                return;
            }

            OnDownloadComicCompleted(new DownloadSingleComicCompletedEventArgs(_comicsDownloaded, currentUrl));

            if (string.IsNullOrEmpty(currentUrl))
            {
                OnDownloadCompleted();
                return;
            }

            if (_comicsDownloaded == _comicsToDownload)
            {
                OnDownloadCompleted();
                return;
            }

            if (_isDownloadCancelled)
                return;

            _url = currentUrl;
            _client.DownloadStringAsync(new Uri(currentUrl));
        }

        /// <summary>
        /// Called when a comic chunk has been downloaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadedComicChunkCallback(object sender, DownloadedFileChunkEventArgs e)
        {
            e.Cancel = _isDownloadCancelled;
        }
        #endregion

        #region DownloadComicCompleted Event
        private object _downloadComicCompletedLock = new object();
        private event EventHandler<DownloadSingleComicCompletedEventArgs> _downloadComicCompleted;
        /// <summary>
        /// Occurs when a single comic is downloaded.
        /// </summary>
        public event EventHandler<DownloadSingleComicCompletedEventArgs> DownloadComicCompleted
        {
            add
            {
                lock (_downloadComicCompletedLock)
                {
                    _downloadComicCompleted += value;
                }
            }
            remove
            {
                lock (_downloadComicCompletedLock)
                {
                    _downloadComicCompleted -= value;
                }
            }
        }

        protected virtual void OnDownloadComicCompleted(DownloadSingleComicCompletedEventArgs e)
        {
            EventHandler<DownloadSingleComicCompletedEventArgs> eventReference = _downloadComicCompleted;

            if (eventReference != null)
                eventReference(this, e);
        }
        #endregion

        #region DownloadCompleted Event
        private object _downloadCompletedLock = new object();
        private event EventHandler _downloadCompleted;
        /// <summary>
        /// Occurs when the entire download is completed.
        /// </summary>
        public event EventHandler DownloadCompleted
        {
            add
            {
                lock (_downloadCompletedLock)
                {
                    _downloadCompleted += value;
                }
            }
            remove
            {
                lock (_downloadCompletedLock)
                {
                    _downloadCompleted -= value;
                }
            }
        }

        protected virtual void OnDownloadCompleted()
        {
            _client.Dispose();

            EventHandler eventReference = _downloadCompleted;

            if (eventReference != null)
                eventReference(this, EventArgs.Empty);
        }
        #endregion
    }
}