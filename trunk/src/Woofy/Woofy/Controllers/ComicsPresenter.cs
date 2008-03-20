using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Collections;
using System.Threading;

using Woofy.Entities;
using Woofy.Services;
using Woofy.Views;
using System.IO;
using Woofy.Other;
using System.Windows.Data;
using System.Windows.Threading;
using Woofy.EventArguments;
using System.Net;
using Woofy.DatabaseAccess;

namespace Woofy.Controllers
{
    public class ComicsPresenter
    {
        private delegate void MethodInvoker();

        #region Properties
        private ComicCollection Comics { get; set; }
        private ComicStripCollection Strips { get; set; }

        public ListCollectionView ActiveComicsView { get; private set; }
        public ListCollectionView InactiveComicsView { get; private set; }

        public ListCollectionView StripsView { get; private set; }
        #endregion

        #region Variables
        private ComicDefinitionsService _comicDefinitionService = new ComicDefinitionsService();
        private FileDownloadService _fileDownloadService = new FileDownloadService();
        private PageParseService _pageParseService;
        private FileWrapper _file;
        private PathWrapper _path;
        private WebClientWrapper _webClient;
        private DatabaseAdapter _databaseAdapter = new DatabaseAdapter();
        #endregion

        #region Constructors
        public ComicsPresenter()
            : this (new PageParseService(), new WebClientWrapper(), new PathWrapper(), new FileWrapper())
        {
        }

        public ComicsPresenter(PageParseService pageParseService, WebClientWrapper webClient, PathWrapper path, FileWrapper file)
        {
            _pageParseService = pageParseService;
            _webClient = webClient;
            _path = path;
            _file = file;
        }
        #endregion

        #region Public Methods
        public void RunApplication()
        {
            ComicDefinitionCollection fileDefinitions = _comicDefinitionService.BuildComicDefinitionsFromFiles();
            foreach (ComicDefinition fileDefinition in fileDefinitions)
                _databaseAdapter.InsertOrUpdateDefinition(fileDefinition);

            int selectedComicIndex = 1;
            Comics = _databaseAdapter.ReadAllComics();
            foreach (Comic comic in Comics)
                comic.FaviconPath = Path.Combine(ApplicationSettings.FaviconsFolder, "blank.png");
            Strips = _databaseAdapter.ReadStripsForComic(Comics[selectedComicIndex]);

            InactiveComicsView = new ListCollectionView(Comics);
            InactiveComicsView.Filter = new Predicate<object>(delegate(object comic)
            {
                return !((Comic)comic).IsActive;
            });

            ActiveComicsView = new ListCollectionView(Comics);
            ActiveComicsView.Filter = new Predicate<object>(delegate(object comic)
            {
                return ((Comic)comic).IsActive;
            });
            
            StripsView = new ListCollectionView(Strips);

            //ThreadPool.QueueUserWorkItem(delegate { CheckActiveComicsForUpdates(); });
            //CheckActiveComicsForUpdates();

            //ThreadPool.UnsafeQueueUserWorkItem(RefreshComicFavicons, null);
            //RefreshComicFavicons(null);

            //SelectComics startWindow = new SelectComics(this);
            ViewComics startWindow = new ViewComics(this);
            Application application = new Application();
            application.Run(startWindow);
        }        

        public void ActivateComics(IList comicsToActivate)
        {
            foreach (Comic comic in comicsToActivate)
                comic.IsActive = true;
        }

        public void DeactivateComics(IList comicsToDeactivate)
        {
            foreach (Comic comic in comicsToDeactivate)
                comic.IsActive = false;
        }

        public void PersistComicsChanges()
        {
        }

        public void DiscardComicsChanges()
        {
        } 
        #endregion

        public void RefreshComicFavicons()
        {
            foreach (Comic comic in Comics)
            {
                RefreshComicFavicon(comic);
            }
        }

        public void RefreshComicFavicon(Comic comic)
        {
            Uri faviconAddress = _pageParseService.RetrieveFaviconAddressFromPage(comic.Definition.HomePageAddress);
            if (faviconAddress == null)
                return;

            string faviconTempPath = _path.GetTempFileName();
            _webClient.DownloadFile(faviconAddress, faviconTempPath);

            string faviconName = comic.Id.ToString() + ".ico";
            string faviconPath = _path.Combine(ApplicationSettings.FaviconsFolder, faviconName);

            _file.Delete(faviconPath);
            _file.Move(faviconTempPath, faviconPath);

            comic.FaviconPath = faviconPath;
            
            RefreshViews();
        }

        private void RefreshViews()
        {
            OnRunCodeOnUIThreadRequired(delegate
            {
                ActiveComicsView.Refresh();
                InactiveComicsView.Refresh();
            });
        }

        private void RefreshStrips()
        {
            OnRunCodeOnUIThreadRequired(delegate
            {
                StripsView.Refresh();
            });
        }

        #region Events - RunCodeOnUIThreadRequired
        private event EventHandler<RunCodeOnUIThreadRequiredEventArgs> _runCodeOnUIThreadRequired;
        public event EventHandler<RunCodeOnUIThreadRequiredEventArgs> RunCodeOnUIThreadRequired
        {
            add { _runCodeOnUIThreadRequired += value; }
            remove { _runCodeOnUIThreadRequired -= value; }
        }

        protected virtual void OnRunCodeOnUIThreadRequired(RunCodeOnUIThreadRequiredEventArgs e)
        {
            EventHandler<RunCodeOnUIThreadRequiredEventArgs> reference = _runCodeOnUIThreadRequired;
            if (reference != null)
                reference(this, e);
        }

        private void OnRunCodeOnUIThreadRequired(MethodInvoker code)
        {
            RunCodeOnUIThreadRequiredEventArgs e = new RunCodeOnUIThreadRequiredEventArgs(code);
            OnRunCodeOnUIThreadRequired(e);
        }

        #endregion

        private void CheckActiveComicsForUpdates()
        {
            foreach (Comic comic in ActiveComicsView)
            {
                CheckComicForUpdates(comic);
            }
        }

        private void CheckComicForUpdates(Comic comic)
        {
            ComicDefinition definition = comic.Definition;
            ComicStrip mostRecentStrip = _databaseAdapter.ReadMostRecentStrip(comic);
            string downloadFolder = _path.Combine(ApplicationSettings.DefaultDownloadFolder, comic.Name);
            try
            {
                Uri startAddress;
                if (mostRecentStrip == null)
                {
                    startAddress = _pageParseService.GetLatestPageOrStartAddress(definition.HomePageAddress, definition.LatestIssueRegex);
                }
                else
                {
                    string pageContent = _webClient.DownloadString(mostRecentStrip.SourcePageAddress);
                    Uri[] nextStripLinks = _pageParseService.RetrieveLinksFromPageByRegex(definition.NextIssueRegex, pageContent, mostRecentStrip.SourcePageAddress);
                    if (nextStripLinks.Length == 0)
                        return;

                    startAddress = nextStripLinks[0];
                }

                Uri currentAddress = startAddress;

                while (true)                
                {
                    //if (_isDownloadCancelled)
                    //{
                    //    downloadOutcome = DownloadOutcome.Cancelled;
                    //    break;
                    //}

                    string pageContent = _webClient.DownloadString(currentAddress);

                    Uri[] comicLinks = _pageParseService.RetrieveLinksFromPageByRegex(definition.StripRegex, pageContent, currentAddress);
                    Uri[] nextStripLinks = _pageParseService.RetrieveLinksFromPageByRegex(definition.NextIssueRegex, pageContent, currentAddress);

                    if (!MatchedLinksObeyRules(comicLinks.Length, definition.AllowMissingStrips, definition.AllowMultipleStrips))//, ref downloadOutcome))
                        break;
                    

                    //bool fileAlreadyDownloaded = false;
                    //string backButtonStringLink = backButtonLink == null ? null : backButtonLink.AbsoluteUri;
                    foreach (Uri comicLink in comicLinks)
                    {
                        string stripFileName = comicLink.AbsoluteUri.Substring(comicLink.AbsoluteUri.LastIndexOf('/') + 1);
                        string downloadPath = _path.Combine(downloadFolder, stripFileName);
                        if (!Directory.Exists(downloadFolder))
                            Directory.CreateDirectory(downloadFolder);
                        _fileDownloadService.DownloadFile(comicLink, downloadPath, currentAddress);

                        ComicStrip strip = new ComicStrip(comic);
                        strip.SourcePageAddress = currentAddress;
                        strip.FilePath = downloadPath;

                        if (ActiveComicsView.CurrentItem == comic)
                        {
                            Strips.Add(strip);
                            RefreshStrips();
                        }

                        _databaseAdapter.InsertStrip(strip);

                        //if (fileAlreadyDownloaded && comicsToDownload == ComicsProvider.AllAvailableComics)    //if the file hasn't been downloaded, then all new comics have been downloaded => exit
                            //break;

                        //OnDownloadComicCompleted(new DownloadStripCompletedEventArgs(i + 1, backButtonStringLink));
                    }

                    //HACK
                    //if (fileAlreadyDownloaded && comicsToDownload == ComicsProvider.AllAvailableComics)    //if the file hasn't been downloaded, then all new comics have been downloaded => exit
                      //  break;

                    if (nextStripLinks.Length == 0)
                        break;

                    currentAddress = nextStripLinks[0];
                }
            }
            catch (UriFormatException ex)
            {
            }
            catch (WebException ex)
            {
                //TODO: trebuie sa raportez exceptiile ca erori.
            }
        }

        /// <summary>
        /// Checks if the matched comic links obey the comic's download rules (i.e. no multiple strip matches).
        /// </summary>
        /// <param name="comicLinks">The list of matched comic links.</param>
        /// <param name="downloadOutcome">If a rule is not obeyed, then this parameter will contain the respective outcome.</param>
        /// <returns>True if the matched links obey the rules, false otherwise.</returns>
        private bool MatchedLinksObeyRules(int linksLength, bool allowMissingStrips, bool allowMultipleStrips)//, ref DownloadOutcome downloadOutcome)
        {
            if (linksLength == 0 && !allowMissingStrips)
            {
                //downloadOutcome = DownloadOutcome.NoStripMatchesRuleBroken;
                return false;
            }

            if (linksLength > 1 && !allowMultipleStrips)
            {
                //downloadOutcome = DownloadOutcome.MultipleStripMatchesRuleBroken;
                return false;
            }

            return true;
        }

        public void HandleSelectedComic(Comic comic)
        {
            Strips.Clear();
            _databaseAdapter.ReadStripsForComic(comic).CopyTo(Strips);
            
            RefreshStrips();
        }

        public void DeleteStrips(IList strips)
        {
            foreach (ComicStrip strip in strips)
            {
                _databaseAdapter.DeleteStrip(strip);
                _file.TryDelete(strip.FilePath);

                Strips.Remove(strip);
            }

            RefreshStrips();
        }
    }
}
