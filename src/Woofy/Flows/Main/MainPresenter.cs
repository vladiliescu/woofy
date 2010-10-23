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
		private readonly IBotSupervisor botSupervisor;
		private readonly IUserSettings userSettings;
		private readonly IDefinitionCompiler compiler;
		private readonly IAppSettings appSettings;
		private readonly IApplicationController applicationController;

		public BindingList<Comic> Comics
		{
			get { return botSupervisor.Comics; }
		}

		public MainPresenter(IApplicationController applicationController, IComicSelectionController comicSelectionController, IComicRepository comicRepository, IBotSupervisor botSupervisor, IUserSettings userSettings, IDefinitionCompiler compiler, IAppSettings appSettings)
		{
			this.applicationController = applicationController;
			this.comicSelectionController = comicSelectionController;
			this.appSettings = appSettings;
			this.compiler = compiler;
			this.userSettings = userSettings;
			this.botSupervisor = botSupervisor;
			this.comicRepository = comicRepository;
		}

		public void AddComicRequested()
		{
			applicationController.Execute<AddComic.AddComic>();
			return;

			var result = comicSelectionController.DisplayComicSelectionForm();
			if (result == DialogResult.Cancel)
				return;

			//refresh the already running comics
			var comics = comicRepository.RetrieveActiveComics();
			for (var i = 0; i < botSupervisor.Comics.Count;)
			{
				var activeComic = botSupervisor.Comics[i];
				var comicIsStillActive = comics.FirstOrDefault(x => x == activeComic) != null;
				if (comicIsStillActive)
				{
					i++;
					continue;
				}

				botSupervisor.Delete(activeComic);
			}

			foreach (var comic in comics)
			{
				var comicIsAlreadyActive = botSupervisor.Comics.FirstOrDefault(x => x == comic) != null;
				if (comicIsAlreadyActive)
					continue;

				botSupervisor.Add(comic);
				botSupervisor.Resume(comic);
			}

			botSupervisor.ResetComicsBindings();
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
				botSupervisor.Toggle(comic);

			botSupervisor.ResetComicsBindings();
		}

		public void StartBots(Comic[] comics)
		{
			foreach (var comic in comics)
				botSupervisor.Resume(comic);

			botSupervisor.ResetComicsBindings();
		}

		public void StartAllBots()
		{
			botSupervisor.StartAllBots();
		}

		public void StopBots(Comic[] comics)
		{
			foreach (var comic in comics)
				botSupervisor.Pause(comic);

			botSupervisor.ResetComicsBindings();
		}

		public void Initialize(MainForm form)
		{
			if (userSettings.AutomaticallyCheckForUpdates)
				UpdateManager.CheckForUpdatesAsync(false, form);

			StartBots(Comics.ToArray());
		}
	}
}