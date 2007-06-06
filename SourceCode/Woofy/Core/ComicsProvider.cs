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
        #region DownloadThreadData Class
        private class DownloadThreadData
        {
            private int _comicsToDownload;
            /// <summary>
            /// Number of comics to download. Pass <see cref="AllAvailableComics"/> in order to download all the available comics.
            /// </summary>
            public int ComicsToDownload
            {
                get { return _comicsToDownload; }
            }

            private string _downloadDirectory;
            /// <summary>
            /// A string representing the name of the directory to which to download the comics. If it doesn't exist, it will be created.
            /// </summary>
            public string DownloadDirectory
            {
                get { return _downloadDirectory; }
            }

            private string _startUrl;
            /// <summary>
            /// The url at which to start the downloading process.
            /// </summary>
            public string StartUrl
            {
                get { return _startUrl; }
            }

            public DownloadThreadData(int comicsToDownload, string downloadDirectory, string startUrl)
            {
                _comicsToDownload = comicsToDownload;
                _downloadDirectory = downloadDirectory;
                _startUrl = startUrl;
            }
        }
        #endregion

        #region Public Properties
        private ComicInfo _comicInfo;
        /// <summary>
        /// Gets the <see cref="ComicInfo"/> instance used.
        /// </summary>
        public ComicInfo ComicInfo
        {
            get { return _comicInfo; }
        }

        /// <summary>
        /// Causes the <see cref="ComicsProvider"/> to get all available comics, instead of a fixed number.
        /// </summary>
        public static int AllAvailableComics
        {
            get { return -1; }
        }
        #endregion

        #region Instance Members
        private Thread _downloadThread;
        private IComicsDownloader _comicsHandler;
        #endregion

        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ComicsProvider"/> class.
        /// </summary>
        /// <param name="comicInfo">An instance of the <see cref="ComicInfo"/> class, used to determine how to get the comic links.</param>
        public ComicsProvider(ComicInfo comicInfo)
            : this(comicInfo, new ComicsDownloader())
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ComicsProvider"/> class.
        /// </summary>
        /// <param name="comicInfo">An instance of the <see cref="ComicInfo"/> class, used to determine how to get the comic links.</param>
        public ComicsProvider(ComicInfo comicInfo, IComicsDownloader comicsHandler)
        {
            _comicInfo = comicInfo;
            _comicsHandler = comicsHandler;
        }
        #endregion

        #region Public Methods

        public void StartDownloadComicsRecursive(int comicsToDownload, string downloadDirectory)
        {
            StartDownloadComicsRecursive(comicsToDownload, downloadDirectory, _comicInfo.StartUrl);
        }

        public void StartDownloadComicsRecursive(int comicsToDownload, string downloadDirectory, string startUrl)
        {
            _downloadThread = new Thread(new ParameterizedThreadStart(DownloadComicsRecursiveFromThread));
            _downloadThread.IsBackground = true;
            _downloadThread.Start(new DownloadThreadData(comicsToDownload, downloadDirectory, startUrl));
        }

        /// <summary>
        /// Downloads the first <see cref="comicsToDownload"/> comic links.
        /// </summary>
        /// <param name="comicsToDownload">Number of comics to download. Pass <see cref="AllAvailableComics"/> in order to download all the available comics.</param>
        /// <param name="downloadDirectory">A string representing the name of the directory to which to download the comics. If it doesn't exist, it will be created.</param>
        public void DownloadComicsRecursive(int comicsToDownload, string downloadDirectory)
        {
            DownloadComicsRecursive(comicsToDownload, downloadDirectory, _comicInfo.StartUrl);
        }

        /// <summary>
        /// Downloads the first <see cref="comicsToDownload"/> comic links.
        /// </summary>
        /// <param name="comicsToDownload">Number of comics to download. Pass <see cref="AllAvailableComics"/> in order to download all the available comics.</param>
        /// <param name="downloadDirectory">A string representing the name of the directory to which to download the comics. If it doesn't exist, it will be created.</param>
        /// <param name="startUrl">The url at which to start the download.</param>
        public void DownloadComicsRecursive(int comicsToDownload, string downloadDirectory, string startUrl)
        {
            if (comicsToDownload != ComicsProvider.AllAvailableComics && comicsToDownload <= 0)
                throw new ArgumentOutOfRangeException("comicsToDownload", "Number of comics to download must be greater than zero or equal to ComicsProvider.AllAvailableComics.");


            using (WebClient client = new WebClient())
            {
                client.Credentials = CredentialCache.DefaultNetworkCredentials;

                WebProxy proxy = null;
                if (!string.IsNullOrEmpty(Settings.Default.ProxyAddress))
                {
                    proxy = new WebProxy(Settings.Default.ProxyAddress, Settings.Default.ProxyPort);
                    proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                    client.Proxy = proxy;
                }

                string currentUrl = startUrl;
                bool fileDownloaded;
                for (int i = 0; i < comicsToDownload || comicsToDownload == ComicsProvider.AllAvailableComics; i++)
                {
                    string pageContent = client.DownloadString(currentUrl);

                    string comicLink = RetrieveComicLinkFromPage(pageContent, _comicInfo);
                    string backButtonLink = RetrieveBackButtonLinkFromPage(pageContent, _comicInfo);

                    if (string.IsNullOrEmpty(comicLink))
                        break;

                    fileDownloaded = _comicsHandler.DownloadComic(comicLink, downloadDirectory, proxy);
                    if (!fileDownloaded && comicsToDownload == ComicsProvider.AllAvailableComics)    //if the file hasn't been downloaded, then all new comics have been downloaded => exit
                        break;

                    if (string.IsNullOrEmpty(backButtonLink))
                    {
                        OnComicDownloaded(new ComicEventArgs(i + 1, string.Empty));
                        break;
                    }

                    currentUrl = backButtonLink;
                    OnComicDownloaded(new ComicEventArgs(i + 1, currentUrl));
                }

                OnAllComicsDownloaded(EventArgs.Empty);
            }
        }

        public void PauseDownload()
        {
            _downloadThread.Abort();
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
                        comicInfo.StartUrl + comicMatch.Groups[Settings.Default.ContentToBeMerged].Value
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
                        comicInfo.StartUrl + backButtonMatch.Groups[Settings.Default.ContentToBeMerged].Value
                        );
                else
                    backButtonLinks.Add(backButtonMatch.Value);
            }

            return backButtonLinks.ToArray();
        }

        private void DownloadComicsRecursiveFromThread(object state)
        {
            DownloadThreadData threadData = (DownloadThreadData)state;
            DownloadComicsRecursive(threadData.ComicsToDownload, threadData.DownloadDirectory, threadData.StartUrl);
        }
        #endregion

        #region ComicDownloaded Event
        private object _comicDownloadedLock = new object();
        private event EventHandler<ComicEventArgs> _comicDownloaded;
        public event EventHandler<ComicEventArgs> ComicDownloaded
        {
            add
            {
                lock (_comicDownloadedLock)
                {
                    _comicDownloaded += value;
                }
            }
            remove
            {
                lock (_comicDownloadedLock)
                {
                    _comicDownloaded -= value;
                }
            }
        }

        private void OnComicDownloaded(ComicEventArgs e)
        {
            EventHandler<ComicEventArgs> eventReference = _comicDownloaded;

            if (eventReference != null)
                eventReference(this, e);
        }
        #endregion

        #region AllComicsDownloaded Event
        private object _allComicsDownloadedLock = new object();
        private event EventHandler _allComicsDownloaded;
        public event EventHandler AllComicsDownloaded
        {
            add
            {
                lock (_allComicsDownloadedLock)
                {
                    _allComicsDownloaded += value;
                }
            }
            remove
            {
                lock (_allComicsDownloadedLock)
                {
                    _allComicsDownloaded -= value;
                }
            }
        }

        protected virtual void OnAllComicsDownloaded(EventArgs e)
        {
            EventHandler eventReference = _allComicsDownloaded;

            if (eventReference != null)
                eventReference(this, e);
        }

        #endregion
    }
}
