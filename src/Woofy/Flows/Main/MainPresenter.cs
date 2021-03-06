using System;
using System.ComponentModel;
using System.Text;
using System.Threading;
using Woofy.Core;
using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;
using System.Linq;
using Woofy.Core.SystemProxies;
using Woofy.Flows.About;
using Woofy.Flows.ApplicationLog;
using Woofy.Flows.AutoUpdate;
using Woofy.Flows.Comics;
using Woofy.Flows.Tray;

namespace Woofy.Flows.Main
{
    public interface IMainPresenter
    {
        BindingList<ComicViewModel> Comics { get; }
        string AppLog { get; }
        bool MinimizeToTray { get; }

        void AddComic();
        void Initialize(MainForm form);
        void Open(string command);
        void ToggleComicState(string comicId);
    	void Remove(string comicId);
        void OpenFolder(string comicId);
    	void Donate();
    	void DisplayAboutScreen();
        void EditComic(string comicId);
    }

    public class MainPresenter : IMainPresenter, INotifyPropertyChanged,
        IEventHandler<ComicActivated>,
        IEventHandler<AppLogEntryAdded>,
        IEventHandler<ComicChanged>,
		IEventHandler<ComicRemoved>,
		ICommandHandler<HideOrShowApplication>
    {
        private readonly IAppController appController;
        private readonly IUiThread uiThread;
        private readonly IComicStore comicStore;
        private readonly IAppLog appLog;
		private readonly IComicViewModelMapper mapper;
        private readonly IComicPath comicPath;
        private readonly IDirectoryProxy directory;
        private readonly IUserSettings settings;
        private readonly IAppInfo appInfo;
        private readonly IAppSettings appSettings;

        public BindingList<ComicViewModel> Comics { get; private set; }

        private readonly object appLogBuilderLock = new object();
        private readonly StringBuilder appLogBuilder = new StringBuilder();
        public string AppLog { get; private set; }

        public bool MinimizeToTray
        {
            get { return settings.MinimizeToTray; }
        }

        private MainForm form;

        public MainPresenter(IAppController appController, IUiThread uiThread, IComicStore comicStore, IAppLog appLog, IComicViewModelMapper mapper, IComicPath comicPath, IDirectoryProxy directory, IUserSettings settings, IAppInfo appInfo, IAppSettings appSettings)
        {
            this.appController = appController;
            this.appSettings = appSettings;
            this.appInfo = appInfo;
            this.settings = settings;
            this.directory = directory;
            this.comicPath = comicPath;
            this.mapper = mapper;
        	this.appLog = appLog;
            this.comicStore = comicStore;
            this.uiThread = uiThread;
        }

        public void AddComic()
        {
            appController.Execute<AddComic>();
        }           

        public void Initialize(MainForm form)
        {
			this.form = form;

            appLog.Send("Woofy {0} (c) {1}", appInfo.Version.ToPrettyString(), appInfo.Company);
            appLog.Send(appSettings.HomePage);
            
            ThreadPool.QueueUserWorkItem(o => appController.Execute<AppUpdateCheck>());
            Comics = new BindingList<ComicViewModel>(
                comicStore
                    .GetActiveComics()
                    .Select<Comic, ComicViewModel>(mapper.MapToViewModel)
                    .ToList()
            );
            appController.Execute<StartAllDownloads>();
        }

        public void Open(string command)
        {
            appController.Execute(new StartProcess(command));
        }

    	public void ToggleComicState(string comicId)
        {
            var comic = comicStore.Find(comicId);
			appController.Execute(new ToggleDownload(comic));
        }

    	public void Remove(string comicId)
    	{
			var comic = comicStore.Find(comicId);
			appController.Execute(new DeactivateComic(comic));
    	}

        public void OpenFolder(string comicId)
        {
            var downloadFolder = comicPath.DownloadFolderFor(comicId);
            if (!directory.Exists(downloadFolder))
                return;
            
            appController.Execute(new StartProcess(downloadFolder));
        }

    	public void Donate()
    	{
			appController.Execute<Donate>();
    	}

    	public void DisplayAboutScreen()
    	{
			appController.Execute<DisplayAboutScreen>();
    	}

        public void EditComic(string comicId)
        {
            appController.Execute(new EditComic(comicId));
        }

        public void Handle(AppLogEntryAdded eventData)
        {
            lock (appLogBuilderLock)
            {
                if (eventData.ComicId.IsNotNullOrEmpty())
                    appLogBuilder.AppendFormat("[{0:T}][{1} {2}] {3}\n", DateTime.Now, eventData.ComicId, eventData.ExpressionName, eventData.Message);
                else
                    appLogBuilder.AppendFormat("[{0:T}] {1}\n", DateTime.Now, eventData.Message);
            }            
            
            uiThread.Send(OnAppLogChanged);
        }

    	public event PropertyChangedEventHandler PropertyChanged;
    	private void OnAppLogChanged()
        {
            var eventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref PropertyChanged, null, null);
            if (eventHandler == null)
                return;

            lock (appLogBuilderLock)
            {
                AppLog = appLogBuilder.ToString();
            }
            
            eventHandler(this, new PropertyChangedEventArgs("AppLog"));
        }

    	public void Handle(ComicActivated eventData)
    	{
    		uiThread.Send(() => Comics.Add(mapper.MapToViewModel(eventData.Comic)));
    	}

    	public void Handle(ComicChanged eventData)
        {
			if (eventData.Comic.Status == Status.Inactive)
				return;

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

    	public void Handle(HideOrShowApplication command)
    	{
			uiThread.Send(() => form.HideOrShow());
    	}
    }
}