using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Woofy.Core;
using Woofy.Gui.ComicSelection;
using System.Linq;

namespace Woofy.Gui.Main
{
	public interface IMainController
	{
		void DisplayComicSelectionForm();

		/// <summary>
		/// Opens the folder associated with the specified task, using Windows Explorer.
		/// </summary>
		void OpenFolder(Comic task);

		void ToggleBotState(Comic[] comics);
		void StartBots(Comic[] comics);
		void StopBots(Comic[] comics);
		BindingList<Comic> Tasks { get; }
	}

	public class MainController : IMainController
	{
		readonly IComicSelectionController comicSelectionController;
		readonly IComicRepository comicRepository;
		readonly IBotSupervisor botSupervisor;

        public MainController(IComicSelectionController comicSelectionController, IComicRepository comicRepository, IBotSupervisor botSupervisor)
		{
			this.comicSelectionController = comicSelectionController;
        	this.botSupervisor = botSupervisor;
        	this.comicRepository = comicRepository;
		}

		public void DisplayComicSelectionForm()
		{
			var result = comicSelectionController.DisplayComicSelectionForm();
			if (result == DialogResult.Cancel)
				return;

            //refresh the already running comics
			var comics = comicRepository.RetrieveActiveComics();
			for (var i = 0; i < botSupervisor.Tasks.Count;)
			{
				var activeComic = botSupervisor.Tasks[i];
				var comicIsStillActive = comics.FirstOrDefault(x => x == activeComic) != null;
				if (comicIsStillActive)
				{
					i++;
					continue;
				}

				botSupervisor.DeleteTask(activeComic);
			}

			foreach (var comic in comics)
			{
				var comicIsAlreadyActive = botSupervisor.Tasks.FirstOrDefault(x => x == comic) != null;
				if (comicIsAlreadyActive)
					continue;

				botSupervisor.AddNewTask(comic);
			}

			botSupervisor.ResetTasksBindings();
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
				botSupervisor.ToggleTaskState(comic);

			botSupervisor.ResetTasksBindings();
		}

		public void StartBots(Comic[] comics)
		{
			foreach (var comic in comics)
				botSupervisor.StartTask(comic);

			botSupervisor.ResetTasksBindings();
		}

		public void StopBots(Comic[] comics)
		{
			foreach (var comic in comics)
				botSupervisor.StopTask(comic);

			botSupervisor.ResetTasksBindings();
		}

		public BindingList<Comic> Tasks
		{
			get { return botSupervisor.Tasks; }
		}
	}
}