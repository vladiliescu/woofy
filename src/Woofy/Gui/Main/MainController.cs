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
		void OpenTaskFolder(Comic task);

		void ToggleSpidersState(Comic[] comics);
		void StartSpiders(Comic[] comics);
		void StopSpiders(Comic[] comics);
		BindingList<Comic> Tasks { get; }
	}

	public class MainController : IMainController
	{
		readonly IComicSelectionController comicSelectionController;
		readonly IComicRepository comicRepository;
		readonly ISpiderSupervisor spiderSupervisor;

        public MainController(IComicSelectionController comicSelectionController, IComicRepository comicRepository, ISpiderSupervisor spiderSupervisor)
		{
			this.comicSelectionController = comicSelectionController;
        	this.spiderSupervisor = spiderSupervisor;
        	this.comicRepository = comicRepository;
		}

		public void DisplayComicSelectionForm()
		{
			var result = comicSelectionController.DisplayComicSelectionForm();
			if (result == DialogResult.Cancel)
				return;

            //refresh the already running comics
			var comics = comicRepository.RetrieveActiveComics();
			for (var i = 0; i < spiderSupervisor.Tasks.Count;)
			{
				var activeComic = spiderSupervisor.Tasks[i];
				var comicIsStillActive = comics.FirstOrDefault(x => x == activeComic) != null;
				if (comicIsStillActive)
				{
					i++;
					continue;
				}

				spiderSupervisor.DeleteTask(activeComic);
			}

			foreach (var comic in comics)
			{
				var comicIsAlreadyActive = spiderSupervisor.Tasks.FirstOrDefault(x => x == comic) != null;
				if (comicIsAlreadyActive)
					continue;

				spiderSupervisor.AddNewTask(comic);
			}

			spiderSupervisor.ResetTasksBindings();
		}

		/// <summary>
		/// Opens the folder associated with the specified task, using Windows Explorer.
		/// </summary>
		public void OpenTaskFolder(Comic task)
		{
			var downloadFolder = (Path.IsPathRooted(task.DownloadFolder) ? task.DownloadFolder : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, task.DownloadFolder));
			if (Directory.Exists(downloadFolder))
				Process.Start(downloadFolder);
		}

		public void ToggleSpidersState(Comic[] comics)
		{
			foreach (var comic in comics)
				spiderSupervisor.ToggleTaskState(comic, false);

			spiderSupervisor.ResetTasksBindings();
		}

		public void StartSpiders(Comic[] comics)
		{
			foreach (var comic in comics)
				spiderSupervisor.StartTask(comic);

			spiderSupervisor.ResetTasksBindings();
		}

		public void StopSpiders(Comic[] comics)
		{
			foreach (var comic in comics)
				spiderSupervisor.StopTask(comic);

			spiderSupervisor.ResetTasksBindings();
		}

		public BindingList<Comic> Tasks
		{
			get { return spiderSupervisor.Tasks; }
		}
	}
}