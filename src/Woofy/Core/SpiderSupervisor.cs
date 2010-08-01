using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using Woofy.Settings;

namespace Woofy.Core
{
	public interface ISpiderSupervisor
	{
		BindingList<Comic> Tasks { get; }
		void Initialize();

		/// <summary>
		/// Adds a new comic to the tasks list and database. Also starts its download.
		/// </summary>
		/// <returns>True if the comic has been added successfully, false otherwise.</returns>
		bool AddNewTask(Comic comic);

		/// <summary>
		/// Stops the specified comic's download and deletes it from the database.
		/// </summary>
		/// <param name="comic"></param>
		void DeleteTask(Comic comic);

		/// <summary>
		/// Pauses/unpauses a task, depending on its current state.
		/// </summary>
		void ToggleTaskState(Comic task, bool resetTasksBindings);

		/// <summary>
		/// Stops the specified comic task.
		/// </summary>
		/// <param name="task">Comic task to stop.</param>
		void StopTask(Comic task);

		/// <summary>
		/// Start the specified comic task.
		/// </summary>
		/// <param name="task">Comic task to start.</param>
		void StartTask(Comic task);

		void ResetTasksBindings();
	}

	public class SpiderSupervisor : ISpiderSupervisor
	{
		public BindingList<Comic> Tasks { get; private set; }

		readonly List<Spider> spiders = new List<Spider>();
		readonly IComicRepository comicRepository;
		readonly SynchronizationContext synchronizationContext;

		public SpiderSupervisor(IComicRepository comicRepository, SynchronizationContext synchronizationContext)
		{
			this.comicRepository = comicRepository;
			this.synchronizationContext = synchronizationContext;
			//I uses a List<Comic> instead of the original array in order to be able to add/remove items to/from the BindingList
			Tasks = new BindingList<Comic>(new List<Comic>(comicRepository.RetrieveActiveComics()));
		}

#warning de mutat in MainController
		public void Initialize()
		{
			foreach (var comic in Tasks)
			{
				AddComicsProviderAndStartDownload(comic);
			}
		}

		/// <summary>
		/// Adds a new comic to the tasks list and database. Also starts its download.
		/// </summary>
		/// <returns>True if the comic has been added successfully, false otherwise.</returns>
		public bool AddNewTask(Comic comic)
		{
			//if (comicStore.RetrieveActiveTasksByComicInfoFile(comic.Definition.Filename).Count > 0)
			//    return false;
			Tasks.Add(comic);
			AddComicsProviderAndStartDownload(comic);

			return true;
		}

		/// <summary>
		/// Stops the specified comic's download and deletes it from the database.
		/// </summary>
		/// <param name="comic"></param>
		public void DeleteTask(Comic comic)
		{
			var index = Tasks.IndexOf(comic);
			var comicsProvider = spiders[index];
			if (comic.Status == TaskStatus.Running)
				comicsProvider.StopDownload();

			Tasks.RemoveAt(index);
			spiders.RemoveAt(index);
		}

		/// <summary>
		/// Pauses/unpauses a task, depending on its current state.
		/// </summary>
		public void ToggleTaskState(Comic task, bool resetTasksBindings)
		{
			switch (task.Status)
			{
				case TaskStatus.Stopped:
					StartTask(task);
					break;
				case TaskStatus.Running:
					StopTask(task);
					break;
			}

			if (resetTasksBindings)
				ResetTasksBindings();
		}

		/// <summary>
		/// Stops the specified comic task.
		/// </summary>
		/// <param name="task">Comic task to stop.</param>
		public void StopTask(Comic task)
		{
			if (task.Status != TaskStatus.Running)
				return;

			int index = Tasks.IndexOf(task);
			Spider spider = spiders[index];

			task.Status = TaskStatus.Stopped;
			spider.StopDownload();

#warning How do I handle this? comicRepository.Update(task);
		}

		/// <summary>
		/// Start the specified comic task.
		/// </summary>
		/// <param name="task">Comic task to start.</param>
		public void StartTask(Comic task)
		{
			if (task.Status != TaskStatus.Stopped)
				return;

			int index = Tasks.IndexOf(task);
			Spider spider = spiders[index];

			task.Status = TaskStatus.Running;
			spider.DownloadComicsAsync(task.CurrentUrl);

			//comicRepository.Update(task);
		}

		public void ResetTasksBindings()
		{
			Tasks.ResetBindings();
		}

		private void AddComicsProviderAndStartDownload(Comic task)
		{
			var comicsProvider = new Spider(task.Definition, task.DownloadFolder, task.RandomPausesBetweenRequests);
			spiders.Add(comicsProvider);

			comicsProvider.DownloadComicCompleted += DownloadComicCompletedCallback;
			comicsProvider.DownloadCompleted += DownloadComicsCompletedCallback;

			if (task.Status == TaskStatus.Finished)
			{
				task.Status = TaskStatus.Running;
				//comicRepository.Update(task);
			}

			if (task.Status != TaskStatus.Running)
				return;

			if (string.IsNullOrEmpty(task.CurrentUrl))
				comicsProvider.DownloadComicsAsync();
			else
				comicsProvider.DownloadComicsAsync(task.CurrentUrl);
		}

		private void DownloadComicCompletedCallback(object sender, DownloadStripCompletedEventArgs e)
		{
			synchronizationContext.Post(o =>
											{
												var provider = (Spider)sender;

												int index = spiders.IndexOf(provider);
												if (index == -1) //in case the task has already been deleted.
													return;

												Comic task = Tasks[index];
												task.DownloadedComics++;
												task.CurrentUrl = e.CurrentUrl;
												//comicRepository.Update(task);

												ResetTasksBindings();
											}, null);
		}

		private void DownloadComicsCompletedCallback(object sender, DownloadCompletedEventArgs e)
		{
			synchronizationContext.Post(o =>
											{
												var comicsProvider = (Spider)sender;

												var index = spiders.IndexOf(comicsProvider);
												if (index == -1) //in case the task has already been deleted.
													return;

												var task = Tasks[index];
												task.Status = e.DownloadOutcome == DownloadOutcome.Cancelled
																? TaskStatus.Stopped
																: TaskStatus.Finished;

												//only set the currentUrl to null if the outcome is successful
												if (e.DownloadOutcome == DownloadOutcome.Successful)
													task.CurrentUrl = null;

												task.DownloadOutcome = e.DownloadOutcome;
												//comicRepository.Update(task);

												ResetTasksBindings();

												if (!UserSettings.CloseWhenAllComicsHaveFinished)
													return;

												var allTasksHaveFinished = true;
												foreach (var _task in Tasks)
												{
													if (_task.Status != TaskStatus.Running)
														continue;

													allTasksHaveFinished = false;
													break;
												}

												if (allTasksHaveFinished)
													Application.Exit();
											}, null);
		}
	}
}