using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using Woofy.Core;
using Woofy.Core.ComicManagement;
using Woofy.Core.Infrastructure;
using System.Linq;
using Woofy.Flows.AddComic;
using Woofy.Flows.ApplicationLog;
using Woofy.Flows.AutoUpdate;
using Woofy.Flows.Download;

namespace Woofy.Flows.Main
{
    public interface IMainPresenter
    {
        BindingList<ComicViewModel> Comics { get; }
        string AppLog { get; }

        void AddComicRequested();
        void OpenFolder(Comic task);
        void Initialize(MainForm form);
        void Open(string command);
    }

    public class MainPresenter : IMainPresenter, INotifyPropertyChanged,
        IEventHandler<ComicActivated>,
        IEventHandler<AppLogEntryAdded>,
        IEventHandler<ComicChanged>
    {
        private readonly IApplicationController applicationController;
        private readonly IUiThread uiThread;
        private readonly IComicStore comicStore;
        private readonly IAppLog appLog;

        public BindingList<ComicViewModel> Comics { get; private set; }

        private readonly StringBuilder appLogBuilder = new StringBuilder();
        public string AppLog
        {
            get { return appLogBuilder.ToString(); }
        }

        public MainPresenter(IApplicationController applicationController, IUiThread uiThread, IComicStore comicStore, IAppLog appLog)
        {
            this.applicationController = applicationController;
            this.appLog = appLog;
            this.comicStore = comicStore;
            this.uiThread = uiThread;
        }

        public void AddComicRequested()
        {
            applicationController.Execute<AddComic.AddComic>();
        }

        /// <summary>
        /// Opens the folder associated with the specified task, using Windows Explorer.
        /// </summary>
        public void OpenFolder(Comic task)
        {
            var downloadFolder = (Path.IsPathRooted(task.DownloadFolder) ? task.DownloadFolder : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, task.DownloadFolder));
            if (Directory.Exists(downloadFolder))
                applicationController.Execute(new StartProcess(downloadFolder));
        }

        public void Initialize(MainForm form)
        {
            appLog.Send("Hello World");

            applicationController.Execute(new CheckForUpdates(form));
            Comics = new BindingList<ComicViewModel>(
                comicStore
                    .GetActiveComics()
                    .Select<Comic, ComicViewModel>(MapToViewModel)
                    .ToList()
            );
            applicationController.Execute<StartAllDownloads>();
        }

        public void Open(string command)
        {
            applicationController.Execute(new StartProcess(command));
        }

        public void Handle(ComicActivated eventData)
        {
            var comic = eventData.Comic;

            uiThread.Send(() => Comics.Add(MapToViewModel(comic)));
        }

        public void Handle(AppLogEntryAdded eventData)
        {
            appLogBuilder.AppendFormat("{0}\n", eventData);
            uiThread.Send(OnAppLogChanged);
        }

        private void MapToViewModel(Comic comic, ComicViewModel viewModel)
        {
            viewModel.Id = comic.Id;
            viewModel.Name = comic.Name;
            viewModel.DownloadedStrips = comic.DownloadedStrips;
            viewModel.Status = comic.Status;
            viewModel.CurrentPage = comic.CurrentPage.AbsoluteUri;
        }

        private ComicViewModel MapToViewModel(Comic comic)
        {
            var viewModel = new ComicViewModel();
            MapToViewModel(comic, viewModel);
            return viewModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnAppLogChanged()
        {
            var eventHandler = PropertyChanged;
            if (eventHandler == null)
                return;

            eventHandler(this, new PropertyChangedEventArgs("AppLog"));
        }

        public void Handle(ComicChanged eventData)
        {
            uiThread.Send(() =>
            {
                var viewModel = Comics.Single(c => c.Id == eventData.Comic.Id);
                MapToViewModel(eventData.Comic, viewModel);
                Comics.ResetItem(Comics.IndexOf(viewModel));
            });
        }
    }
}