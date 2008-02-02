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

namespace Woofy.Controllers
{
    public class ComicsPresenter : IPresenter
    {
        #region Properties
        public ComicCollection Comics { get; private set; } 
        #endregion

        #region Variables
        private IComicPersistanceService _comicPersistanceService;
        private IComicDefinitionsService _comicDefinitionService = new ComicDefinitionsService();
        private FileDownloadService _fileDownloadService = new FileDownloadService();
        private PageParseService _pageParseService;
        private FileWrapper _file;
        private PathWrapper _path;
        private WebClientWrapper _webClient;
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
            Comics = _comicDefinitionService.BuildComicsFromDefinitions();
            foreach (Comic comic in Comics)
            {
                comic.FaviconPath = Path.Combine(Constants.FaviconsFolder, "blank.png");
            }

            ThreadPool.UnsafeQueueUserWorkItem(RefreshComicFavicons, null);
            //RefreshComicFavicons(null);

            SelectComics startWindow = new SelectComics(this);
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


        public void RefreshComicFavicons(object state)
        {
            foreach (Comic comic in Comics)
            {
                RefreshComicFavicon(comic);
            }
        }

        public void RefreshComicFavicon(Comic comic)
        {
            Uri faviconAddress = _pageParseService.RetrieveFaviconAddressFromPage(comic.HomePageUrl);
            if (faviconAddress == null)
                return;

            string faviconTempPath = _path.GetTempFileName();
            _webClient.DownloadFile(faviconAddress, faviconTempPath);

            string faviconName = _path.GetFileNameWithoutExtension(comic.DefinitionFileName) + ".ico";
            string faviconPath = _path.Combine(Constants.FaviconsFolder, faviconName);

            _file.Delete(faviconPath);
            _file.Move(faviconTempPath, faviconPath);

            comic.FaviconPath = faviconPath;
            OnRefreshViewsRequired();
        }


        private event EventHandler _refreshViewsRequired;
        public event EventHandler RefreshViewsRequired
        {
            add { _refreshViewsRequired += value; }
            remove { _refreshViewsRequired -= value; }
        }

        protected virtual void OnRefreshViewsRequired(EventArgs e)
        {
            EventHandler reference = _refreshViewsRequired;
            if (reference != null)
                reference(this, e);
        }

        private void OnRefreshViewsRequired()
        {
            OnRefreshViewsRequired(EventArgs.Empty);
        }
    }
}
