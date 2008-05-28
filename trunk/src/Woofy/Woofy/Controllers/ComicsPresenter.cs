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
        private ComicDefinitionsService _comicDefinitionService = new ComicDefinitionsService();
        private DatabaseAdapter _databaseAdapter = new DatabaseAdapter();
        private ComicAdapter _comicAdapter = new ComicAdapter();
        private FileWrapper _file = new FileWrapper();
        #endregion

        public ComicsPresenter()
        {
            _comicAdapter.DownloadingStrip += new EventHandler<DownloadingStripEventArgs>(OnDownloadingStrip);
            _comicAdapter.DownloadedStrip += new EventHandler<DownloadedStripEventArgs>(OnDownloadedStrip);
            _comicAdapter.DownloadedAllStripsFromPage += new EventHandler<DownloadedAllStripsFromPageEventArgs>(OnDownloadedAllStripsFromPage);
        }

        private void OnDownloadedAllStripsFromPage(object sender, DownloadedAllStripsFromPageEventArgs e)
        {
            Comic downloadingComic = e.Comic;
            Comic highestPriorityComic = GetHighestPriorityPendingComic();

            if (downloadingComic == highestPriorityComic)
                return;

            ComicStrip strip = _databaseAdapter.ReadMostRecentStrip(highestPriorityComic);
            e.NextStrip = strip;
        }

        private Comic GetHighestPriorityPendingComic()
        {
            
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
                comic.FaviconPath = Path.Combine(ApplicationSettings.FaviconsFolder, "blank.png");
            Strips = _databaseAdapter.ReadStripsForComic(Comics[selectedComicIndex]);

            InactiveComicsView = new ListCollectionView(Comics);
            InactiveComicsView.Filter = (comic => !((Comic)comic).IsActive);

            ActiveComicsView = new ListCollectionView(Comics);
            ActiveComicsView.Filter = (comic => ((Comic)comic).IsActive);

            ActiveAndSortedComicsView = new ListCollectionView(Comics);
            ActiveAndSortedComicsView.Filter = (comic => ((Comic)comic).IsActive);
            ActiveAndSortedComicsView.SortDescriptions.Add(new SortDescription("Priority", ListSortDirection.Descending));
            ActiveAndSortedComicsView.MoveCurrentToFirst();

            StripsView = new ListCollectionView(Strips);

            ThreadPool.QueueUserWorkItem(delegate { CheckActiveComicsForUpdates(); });
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
            OnRunCodeOnUIThreadRequired(delegate
            {
                ActiveComicsView.Refresh();
                ActiveAndSortedComicsView.Refresh();
                InactiveComicsView.Refresh();
            });
        }

        [Obsolete("Ar trebui sa vad daca pot apela Strips.Add pe ui thread. idem refreshviews.")]
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
