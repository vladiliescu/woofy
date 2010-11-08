using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using Woofy.Core;
using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;
using System.Linq;
using Woofy.Flows.AddComic;
using Woofy.Flows.ApplicationLog;
using Woofy.Flows.AutoUpdate;

namespace Woofy.Flows.Main
{
    public interface IMainPresenter
    {
        BindingList<ComicViewModel> Comics { get; }
        string AppLog { get; }

        void AddComic();
        void Initialize(MainForm form);
        void Open(string command);
        void ToggleComicState(string comicId);
    	void Remove(string comicId);
    }

    public class MainPresenter : IMainPresenter, INotifyPropertyChanged,
        IEventHandler<ComicActivated>,
        IEventHandler<AppLogEntryAdded>,
        IEventHandler<ComicChanged>,
		IEventHandler<ComicRemoved>
    {
        private readonly IApplicationController applicationController;
        private readonly IUiThread uiThread;
        private readonly IComicStore comicStore;
        private readonly IAppLog appLog;
		private readonly IComicViewModelMapper mapper;

        public BindingList<ComicViewModel> Comics { get; private set; }

        private readonly StringBuilder appLogBuilder = new StringBuilder();
        public string AppLog
        {
            get { return appLogBuilder.ToString(); }
        }

        public MainPresenter(IApplicationController applicationController, IUiThread uiThread, IComicStore comicStore, IAppLog appLog, IComicViewModelMapper mapper)
        {
            this.applicationController = applicationController;
        	this.mapper = mapper;
        	this.appLog = appLog;
            this.comicStore = comicStore;
            this.uiThread = uiThread;
        }

        public void AddComic()
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
                    .Select<Comic, ComicViewModel>(mapper.MapToViewModel)
                    .ToList()
            );
            applicationController.Execute<StartAllDownloads>();
        }

        public void Open(string command)
        {
            applicationController.Execute(new StartProcess(command));
        }

    	public void ToggleComicState(string comicId)
        {
            var comic = comicStore.Find(comicId);
			applicationController.Execute(new ToggleDownload(comic));
        }

    	public void Remove(string comicId)
    	{
			var comic = comicStore.Find(comicId);
			applicationController.Execute(new DeactivateComic(comic));
    	}

    	public void Handle(AppLogEntryAdded eventData)
        {
            appLogBuilder.AppendFormat("{0}\n", eventData);
            uiThread.Send(OnAppLogChanged);
        }

    	public event PropertyChangedEventHandler PropertyChanged;
    	private void OnAppLogChanged()
        {
            var eventHandler = PropertyChanged;
            if (eventHandler == null)
                return;

            eventHandler(this, new PropertyChangedEventArgs("AppLog"));
        }

    	public void Handle(ComicActivated eventData)
    	{
    		var comic = eventData.Comic;

    		uiThread.Send(() => Comics.Add(mapper.MapToViewModel(comic)));
    	}

    	public void Handle(ComicChanged eventData)
        {
            uiThread.Send(() =>
            {
                var viewModel = Comics.SingleOrDefault(c => c.Id == eventData.Comic.Id);
				if (viewModel == null)	//in case the comic is removed before the ComicChanged event has fired.
					return;

                mapper.MapToViewModel(eventData.Comic, viewModel);
                Comics.ResetItem(Comics.IndexOf(viewModel));
            });
        }

    	public void Handle(ComicRemoved eventData)
    	{
			uiThread.Send(() =>
			{
				var viewModel = Comics.SingleOrDefault(c => c.Id == eventData.Comic.Id);
				Comics.Remove(viewModel);
			});
    	}
    }
}