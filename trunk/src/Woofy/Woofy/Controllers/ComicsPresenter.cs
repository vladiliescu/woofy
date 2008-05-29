using System;
using System.Windows;
using System.Collections;
using System.Threading;
using Woofy.Core;
using Woofy.Entities;
using Woofy.Views;
using System.IO;
using Woofy.Other;
using System.Windows.Data;
using Woofy.EventArguments;
using Woofy.DatabaseAccess;
using System.ComponentModel;

namespace Woofy.Controllers
{
    public class ComicsPresenter
    {
        #region Properties
        private ComicCollection Comics { get; set; }
        private ComicStripCollection Strips { get; set; }

        public ListCollectionView ActiveComicsView { get; private set; }
        public ListCollectionView ActiveAndSortedComicsView { get; private set; }
        public ListCollectionView InactiveComicsView { get; private set; }

        public ListCollectionView StripsView { get; private set; }
        #endregion

        #region Variables
        private ComicDefinitionsService _comicDefinitionService;
        private DatabaseAdapter _databaseAdapter;
        private ComicAdapter _comicAdapter;
        private FileWrapper _file;
        private PathWrapper _path;
        #endregion

        public ComicsPresenter()
            : this (new ComicDefinitionsService(), new DatabaseAdapter(), new ComicAdapter(), new FileWrapper(), new PathWrapper())
        {
        }

        public ComicsPresenter(ComicDefinitionsService comicDefinitionsService, DatabaseAdapter databaseAdapter, ComicAdapter comicAdapter, FileWrapper fileWrapper, PathWrapper pathWrapper)
        {
            _comicDefinitionService = comicDefinitionsService;
            _databaseAdapter = databaseAdapter;
            _comicAdapter = comicAdapter;
            _file = fileWrapper;
            _path = pathWrapper;

            _comicAdapter.DownloadingStrip += OnDownloadingStrip;
            _comicAdapter.DownloadedStrip += OnDownloadedStrip;
            _comicAdapter.DownloadedAllStripsFromPage += OnDownloadedAllStripsFromPage;
        }

        private void OnDownloadedAllStripsFromPage(object sender, DownloadedAllStripsFromPageEventArgs e)
        {
            //Comic downloadingComic = e.Comic;
            //Comic highestPriorityComic = GetHighestPriorityPendingComic();

            //if (downloadingComic == highestPriorityComic)
            //    return;

            //ComicStrip strip = _databaseAdapter.ReadMostRecentStrip(highestPriorityComic);
            //e.NextStrip = strip;
        }

        private Comic GetHighestPriorityPendingComic()
        {
            return null;
        }

        private void OnDownloadedStrip(object sender, DownloadedStripEventArgs e)
        {
            _databaseAdapter.InsertStrip(e.Strip);
            if (ActiveAndSortedComicsView.CurrentItem != e.Strip.Comic)
                return;

            Strips.Add(e.Strip);
            RefreshStrips();
        }

        private void OnDownloadingStrip(object sender, DownloadingStripEventArgs e)
        {
            
        }

        #region Public Methods
        public void RunApplication()
        {
            ComicDefinitionCollection fileDefinitions = _comicDefinitionService.BuildComicDefinitionsFromFiles();
            foreach (ComicDefinition fileDefinition in fileDefinitions)
                _databaseAdapter.InsertOrUpdateComicAndDefinition(fileDefinition);

            int selectedComicIndex = 1;
            Comics = _databaseAdapter.ReadAllComics();
            foreach (Comic comic in Comics)
                comic.IconPath = _path.GetFaviconPath("blank.png");
            Strips = _databaseAdapter.ReadStripsForComic(Comics[selectedComicIndex]);
            Comics[0].IconPath = _path.GetFaviconPath("downloading.png");

            InactiveComicsView = new ListCollectionView(Comics);
            InactiveComicsView.Filter = (comic => !((Comic)comic).IsActive);

            ActiveComicsView = new ListCollectionView(Comics);
            ActiveComicsView.Filter = (comic => ((Comic)comic).IsActive);

            ActiveAndSortedComicsView = new ListCollectionView(Comics);
            ActiveAndSortedComicsView.Filter = (comic => ((Comic)comic).IsActive);
            ActiveAndSortedComicsView.SortDescriptions.Add(new SortDescription("Priority", ListSortDirection.Descending));
            ActiveAndSortedComicsView.MoveCurrentToFirst();

            StripsView = new ListCollectionView(Strips);

            //ThreadPool.QueueUserWorkItem(delegate { CheckActiveComicsForUpdates(); });
            //CheckActiveComicsForUpdates();

            //ThreadPool.UnsafeQueueUserWorkItem(RefreshComicFavicons, null);
            //RefreshComicFavicons(null);

            //SelectComics startWindow = new SelectComics(this);
            ViewComics startWindow = new ViewComics(this);
            Application application = new Application();
            application.Run(startWindow);
            //application.Run(new Window1());
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

        #endregion

        private void RefreshComicFavicons()
        {
            foreach (Comic comic in Comics)
            {
                _comicAdapter.RefreshComicFavicon(comic);
                RefreshViews();
            }
        }

        private void RefreshViews()
        {
            OnRunCodeOnUIThread(delegate
            {
                ActiveComicsView.Refresh();
                ActiveAndSortedComicsView.Refresh();
                InactiveComicsView.Refresh();
            });
        }

        [Obsolete("Ar trebui sa vad daca pot apela Strips.Add pe ui thread. idem refreshviews.")]
        private void RefreshStrips()
        {
            OnRunCodeOnUIThread(() => StripsView.Refresh());
        }

        #region Events - RunCodeOnUIThread
        private event EventHandler<RunCodeOnUIThreadRequiredEventArgs> _runCodeOnUIThread;
        public event EventHandler<RunCodeOnUIThreadRequiredEventArgs> RunCodeOnUIThread
        {
            add { _runCodeOnUIThread += value; }
            remove { _runCodeOnUIThread -= value; }
        }

        protected virtual void OnRunCodeOnUIThread(RunCodeOnUIThreadRequiredEventArgs e)
        {
            EventHandler<RunCodeOnUIThreadRequiredEventArgs> reference = _runCodeOnUIThread;
            if (reference != null)
                reference(this, e);
        }

        private void OnRunCodeOnUIThread(MethodInvoker code)
        {
            RunCodeOnUIThreadRequiredEventArgs e = new RunCodeOnUIThreadRequiredEventArgs(code);
            OnRunCodeOnUIThread(e);
        }

        #endregion

        private void CheckActiveComicsForUpdates()
        {
            Comic comic = (Comic)ActiveAndSortedComicsView.GetItemAt(0);
            ComicStrip mostRecentStrip = _databaseAdapter.ReadMostRecentStrip(comic);
            _comicAdapter.CheckComicForUpdates(comic, mostRecentStrip);
        }

        public void HandleSelectedComic(Comic comic)
        {
            Strips.Clear();
            _databaseAdapter.ReadStripsForComic(comic).CopyTo(Strips);

            RefreshStrips();
        }

        public void HandleSelectedStrip(ComicStrip strip)
        {


        }

        public void MoveToNextStrip()
        {
            if (StripsView.CurrentPosition == StripsView.Count - 1)
                return;

            StripsView.MoveCurrentToNext();
        }

        public void MoveToPreviousStrip()
        {
            if (StripsView.CurrentPosition == 0)
                return;

            StripsView.MoveCurrentToPrevious();
        }

        public void MoveToFirstStrip()
        {
            StripsView.MoveCurrentToFirst();
        }

        public void MoveToLastStrip()
        {
            StripsView.MoveCurrentToLast();
        }

        public void DeleteStrips(IList strips)
        {
            foreach (ComicStrip strip in strips)
            {
                _databaseAdapter.DeleteStrip(strip);
                Strips.Remove(strip);
            }

            RefreshStrips();
        }

        public void MoveComicUp(Comic comic)
        {
            int index = ActiveAndSortedComicsView.IndexOf(comic);
            if (index == 0)
                return;

            Comic comicAbove = (Comic)ActiveAndSortedComicsView.GetItemAt(index - 1);
            int tempPriority = comicAbove.Priority;
            comicAbove.Priority = comic.Priority;
            comic.Priority = tempPriority;

            RefreshViews();
        }

        public void MoveComicDown(Comic comic)
        {
            int index = ActiveAndSortedComicsView.IndexOf(comic);
            if (index == ActiveAndSortedComicsView.Count - 1)
                return;

            Comic comicAbove = (Comic)ActiveAndSortedComicsView.GetItemAt(index + 1);
            int tempPriority = comicAbove.Priority;
            comicAbove.Priority = comic.Priority;
            comic.Priority = tempPriority;

            RefreshViews();
        }
    }
}
