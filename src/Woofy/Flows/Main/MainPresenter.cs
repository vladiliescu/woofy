using System.ComponentModel;
using System.Text;
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
    }

    public class MainPresenter : IMainPresenter, INotifyPropertyChanged,
        IEventHandler<ComicActivated>,
        IEventHandler<AppLogEntryAdded>,
        IEventHandler<ComicChanged>,
		IEventHandler<ComicRemoved>,
		ICommandHandler<HideOrShowApplication>
    {
        private readonly IApplicationController applicationController;
        private readonly IUiThread uiThread;
        private readonly IComicStore comicStore;
        private readonly IAppLog appLog;
		private readonly IComicViewModelMapper mapper;
        private readonly IPathRepository pathRepository;
        private readonly IDirectoryProxy directory;
        private readonly IUserSettings settings;

        public BindingList<ComicViewModel> Comics { get; private set; }

        private readonly StringBuilder appLogBuilder = new StringBuilder();
        public string AppLog
        {
            get { return appLogBuilder.ToString(); }
        }

        public bool MinimizeToTray
        {
            get { return settings.MinimizeToTray; }
        }

        private MainForm form;

        public MainPresenter(IApplicationController applicationController, IUiThread uiThread, IComicStore comicStore, IAppLog appLog, IComicViewModelMapper mapper, IPathRepository pathRepository, IDirectoryProxy directory, IUserSettings settings)
        {
            this.applicationController = applicationController;
            this.settings = settings;
            this.directory = directory;
            this.pathRepository = pathRepository;
            this.mapper = mapper;
        	this.appLog = appLog;
            this.comicStore = comicStore;
            this.uiThread = uiThread;
        }

        public void AddComic()
        {
            applicationController.Execute<AddComic>();
        }           

        public void Initialize(MainForm form)
        {
			this.form = form;

            appLog.Send("Hello World");

            applicationController.Execute<AppUpdateCheck>();
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

        public void OpenFolder(string comicId)
        {
            var downloadFolder = pathRepository.DownloadFolderFor(comicId);
            if (!directory.Exists(downloadFolder))
                return;
            
            applicationController.Execute(new StartProcess(downloadFolder));
        }

    	public void Donate()
    	{
			applicationController.Execute<Donate>();
    	}

    	public void DisplayAboutScreen()
    	{
			applicationController.Execute<DisplayAboutScreen>();
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