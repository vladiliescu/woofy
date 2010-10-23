using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Woofy.Core;
using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;
using Woofy.Gui.ComicSelection;
using System.Linq;
using Woofy.Updates;

namespace Woofy.Flows.Main
{
	public interface IMainPresenter
	{
		void AddComicRequested();

		/// <summary>
		/// Opens the folder associated with the specified task, using Windows Explorer.
		/// </summary>
		void OpenFolder(Comic task);

		void ToggleBotState(Comic[] comics);
		void StartBots(Comic[] comics);
		void StopBots(Comic[] comics);
		BindingList<Comic> Comics { get; }
		void Initialize(MainForm form);
	}

	public class MainPresenter : IMainPresenter
	{
		private readonly IComicSelectionController comicSelectionController;
		private readonly IComicRepository comicRepository;
		private readonly IWorkerSupervisor workerSupervisor;
		private readonly IUserSettings userSettings;
		private readonly IDefinitionCompiler compiler;
		private readonly IAppSettings appSettings;
		private readonly IApplicationController applicationController;

		public BindingList<Comic> Comics
		{
			get { return workerSupervisor.Comics; }
		}

		public MainPresenter(IApplicationController applicationController, IComicSelectionController comicSelectionController, IComicRepository comicRepository, IWorkerSupervisor workerSupervisor, IUserSettings userSettings, IDefinitionCompiler compiler, IAppSettings appSettings)
		{
			this.applicationController = applicationController;
			this.comicSelectionController = comicSelectionController;
			this.appSettings = appSettings;
			this.compiler = compiler;
			this.userSettings = userSettings;
			this.workerSupervisor = workerSupervisor;
			this.comicRepository = comicRepository;
		}

		public void AddComicRequested()
		{
			applicationController.Execute<AddComic.AddComic>();
			return;

			//var result = comicSelectionController.DisplayComicSelectionForm();
			//if (result == DialogResult.Cancel)
			//    return;

			////refresh the already running comics
			//var comics = comicRepository.RetrieveActiveComics();
			//for (var i = 0; i < workerSupervisor.Comics.Count;)
			//{
			//    var activeComic = workerSupervisor.Comics[i];
			//    var comicIsStillActive = comics.FirstOrDefault(x => x == activeComic) != null;
			//    if (comicIsStillActive)
			//    {
			//        i++;
			//        continue;
			//    }

			//    workerSupervisor.Delete(activeComic);
			//}

			//foreach (var comic in comics)
			//{
			//    var comicIsAlreadyActive = workerSupervisor.Comics.FirstOrDefault(x => x == comic) != null;
			//    if (comicIsAlreadyActive)
			//        continue;

			//    workerSupervisor.Add(comic);
			//    workerSupervisor.Resume(comic);
			//}

			//workerSupervisor.ResetComicsBindings();
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

		public void ToggleBotState(Comic[] comics)
		{
			foreach (var comic in comics)
				workerSupervisor.Toggle(comic);

			workerSupervisor.ResetComicsBindings();
		}

		public void StartBots(Comic[] comics)
		{
			foreach (var comic in comics)
				workerSupervisor.Resume(comic);

			workerSupervisor.ResetComicsBindings();
		}

		public void StartAllBots()
		{
			workerSupervisor.StartAllBots();
		}

		public void StopBots(Comic[] comics)
		{
			foreach (var comic in comics)
				workerSupervisor.Pause(comic);

			workerSupervisor.ResetComicsBindings();
		}

		public void Initialize(MainForm form)
		{
			if (userSettings.AutomaticallyCheckForUpdates)
				UpdateManager.CheckForUpdatesAsync(false, form);

			StartBots(Comics.ToArray());
		}
	}
}