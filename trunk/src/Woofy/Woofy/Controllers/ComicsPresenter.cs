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
        private PageParseService _pageParseService = new PageParseService();
        private FileWrapper _fileWrapper = new FileWrapper();
        private PathWrapper _pathWrapper = new PathWrapper();
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

        #region Private Methods
        public void RefreshComicFavicons(object state)
        {
            foreach (Comic comic in Comics)
            {
                Uri faviconUrl = _pageParseService.RetrieveFaviconUrlFromPage(comic.HomePageUrl);
                if (faviconUrl == null)
                    continue;

                string faviconTempPath = _fileDownloadService.DownloadFile(faviconUrl, Constants.FaviconsFolder, true);
                //todo: trebuie sa fac overwrite
                string faviconName = comic.Name.Replace(" ", "");
                string faviconPath =_pathWrapper.Combine(_pathWrapper.GetDirectoryName(faviconTempPath), faviconName + ".ico");
                
                _fileWrapper.Move(faviconTempPath, faviconPath);
                comic.FaviconPath = faviconPath;

                OnRefreshViewsRequired();
            }
        }
        #endregion

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
