using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using Woofy.Core;
using Woofy.Core.ComicManagement;
using Woofy.Core.Infrastructure;
using System.Linq;
using Woofy.Flows.AddComic;
using Woofy.Flows.ApplicationLog;
using Woofy.Flows.AutoUpdate;

namespace Woofy.Flows.Main
{
	public interface IMainPresenter
	{
        BindingList<Comic> Comics { get; }
        string AppLog { get; }

		void AddComicRequested();
		void OpenFolder(Comic task);
		void Initialize(MainForm form);
	}

	public class MainPresenter : IMainPresenter, IEventHandler<ComicActivated>, IEventHandler<AppLogEntryAdded>, INotifyPropertyChanged
	{
		private readonly IApplicationController applicationController;
        private readonly IUiThread uiThread;
	    private readonly IComicStore comicStore;
        private readonly IAppLog appLog;

	    public BindingList<Comic> Comics { get; private set; }

        private const string appLogFormat = "{0}\n";
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
				Process.Start(downloadFolder);
		}

		public void Initialize(MainForm form)
		{
            appLog.Send("Hello World");

            applicationController.Execute(new CheckForUpdates(form));
            Comics = new BindingList<Comic>(comicStore.GetActiveComics().ToList());
            applicationController.Execute<StartAllDownloads>();
		}

        public void Handle(ComicActivated eventData)
        {
            var comic = eventData.Comic;

            uiThread.Send(() => Comics.Add(comic));
        }

	    public void Handle(AppLogEntryAdded eventData)
	    {
            appLogBuilder.AppendFormat(appLogFormat, eventData.Message);
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
	}
}