using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Woofy.Core;
using Woofy.Core.ComicManagement;
using Woofy.Core.Infrastructure;
using System.Linq;
using Woofy.Flows.AddComic;
using Woofy.Flows.AutoUpdate;

namespace Woofy.Flows.Main
{
	public interface IMainPresenter
	{
		void AddComicRequested();

		/// <summary>
		/// Opens the folder associated with the specified task, using Windows Explorer.
		/// </summary>
		void OpenFolder(Comic task);
		
		BindingList<Comic> Comics { get; }
		void Initialize(MainForm form);
	}

	public class MainPresenter : IMainPresenter, IEventHandler<ComicActivated>
	{
		private readonly IWorkerSupervisor workerSupervisor;
		private readonly IApplicationController applicationController;
        private readonly IUiThread uiThread;
	    private readonly IComicStore comicStore;

	    public BindingList<Comic> Comics { get; private set; }

		public MainPresenter(IApplicationController applicationController, IWorkerSupervisor workerSupervisor, IUserSettings userSettings, IUiThread uiThread, IComicStore comicStore)
		{
			this.applicationController = applicationController;
		    this.comicStore = comicStore;
		    this.uiThread = uiThread;
			this.workerSupervisor = workerSupervisor;
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
            applicationController.Execute(new CheckForUpdates(form));
            Comics = new BindingList<Comic>(comicStore.GetActiveComics().ToList());
            applicationController.Execute<StartAllDownloads>();
		}

        public void Handle(ComicActivated eventData)
        {
            var comic = eventData.Comic;

            uiThread.Send(() => Comics.Add(comic));
        }
	}
}