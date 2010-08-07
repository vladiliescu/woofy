using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using Woofy.Settings;

namespace Woofy.Core
{
	public interface IBotSupervisor
	{
		BindingList<Comic> Tasks { get; }

		/// <summary>
		/// Adds a new comic to the tasks list and database. Also starts its download.
		/// </summary>
		void AddNewTask(Comic comic);

		/// <summary>
		/// Stops the specified comic's download and deletes it from the database.
		/// </summary>
		void DeleteTask(Comic comic);

		/// <summary>
		/// Pauses/unpauses a comic, depending on its current state.
		/// </summary>
		void ToggleTaskState(Comic comic);
		void StopTask(Comic comic);
		void StartTask(Comic comic);

		void ResetTasksBindings();
	}

	public class BotSupervisor : IBotSupervisor
	{
		public BindingList<Comic> Tasks { get; private set; }

		readonly List<Bot> bots = new List<Bot>();
		readonly IComicRepository comicRepository;
		readonly SynchronizationContext synchronizationContext;

		public BotSupervisor(IComicRepository comicRepository, SynchronizationContext synchronizationContext)
		{
			this.comicRepository = comicRepository;
			this.synchronizationContext = synchronizationContext;
			//I use a List<Comic> instead of the original array in order to be able to add/remove items to/from the BindingList
			Tasks = new BindingList<Comic>(new List<Comic>(comicRepository.RetrieveActiveComics()));
		}

		/// <summary>
		/// Adds a new comic to the tasks list and database. Also starts its download.
		/// </summary>
		/// <returns>True if the comic has been added successfully, false otherwise.</returns>
		public void AddNewTask(Comic comic)
		{
			Tasks.Add(comic);
			AddComicsProviderAndStartDownload(comic);
		}

		/// <summary>
		/// Stops the specified comic's download and deletes it from the database.
		/// </summary>
		/// <param name="comic"></param>
		public void DeleteTask(Comic comic)
		{
			var index = Tasks.IndexOf(comic);
			var comicsProvider = bots[index];
			if (comic.Status == TaskStatus.Running)
				comicsProvider.StopDownload();

			Tasks.RemoveAt(index);
			bots.RemoveAt(index);
		}

		/// <summary>
		/// Pauses/unpauses a comic, depending on its current state.
		/// </summary>
		public void ToggleTaskState(Comic comic)
		{
			switch (comic.Status)
			{
				case TaskStatus.Stopped:
					StartTask(comic);
					break;
				case TaskStatus.Running:
					StopTask(comic);
					break;
			}
		}

		public void StopTask(Comic comic)
		{
			if (comic.Status != TaskStatus.Running)
				return;

			int index = Tasks.IndexOf(comic);
			Bot bot = bots[index];

			comic.Status = TaskStatus.Stopped;
			bot.StopDownload();

#warning How do I handle this? comicRepository.Update(comic);
		}

		public void StartTask(Comic comic)
		{
			if (comic.Status != TaskStatus.Stopped)
				return;

			int index = Tasks.IndexOf(comic);
			Bot bot = bots[index];

			comic.Status = TaskStatus.Running;
			bot.DownloadComicsAsync(comic.CurrentUrl);

			//comicRepository.Update(comic);
		}

		public void ResetTasksBindings()
		{
			Tasks.ResetBindings();
		}

		private void AddComicsProviderAndStartDownload(Comic task)
		{
			var bot = new Bot(task.Definition, task.DownloadFolder, task.RandomPausesBetweenRequests);
			bots.Add(bot);

			bot.DownloadComicCompleted += DownloadComicCompletedCallback;
			bot.DownloadCompleted += DownloadComicsCompletedCallback;

			if (task.Status == TaskStatus.Finished)
			{
				task.Status = TaskStatus.Running;
				//comicRepository.Update(task);
			}

			if (task.Status != TaskStatus.Running)
				return;

			if (string.IsNullOrEmpty(task.CurrentUrl))
				bot.DownloadComicsAsync();
			else
				bot.DownloadComicsAsync(task.CurrentUrl);
		}

		private void DownloadComicCompletedCallback(object sender, DownloadStripCompletedEventArgs e)
		{
			synchronizationContext.Post(o =>
											{
												var provider = (Bot)sender;

												int index = bots.IndexOf(provider);
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
												var comicsProvider = (Bot)sender;

												var index = bots.IndexOf(comicsProvider);
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