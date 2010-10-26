using System.Collections.Generic;
using System.ComponentModel;
using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;
using Woofy.Flows.AddComic;
using System.Linq;
using MoreLinq;

namespace Woofy.Core
{
	public interface IWorkerSupervisor
	{
		/// <summary>
		/// Adds a new comic to the tasks list and database. Also starts its download.
		/// </summary>
		void Add(Comic comic);

		/// <summary>
		/// Stops the specified comic's download and deletes it from the database.
		/// </summary>
		void Delete(Comic comic);

		/// <summary>
		/// Pauses/unpauses a comic, depending on its current state.
		/// </summary>
		void Toggle(Comic comic);
		void Pause(Comic comic);
		void Resume(Comic comic);

		void StartAllBots();
	}

	public class WorkerSupervisor : IWorkerSupervisor
	{
		private readonly List<Definition> workers = new List<Definition>();
		private readonly IComicStore comicStore;

		public WorkerSupervisor(IComicStore comicStore)
		{
			this.comicStore = comicStore;

			var comics = comicStore.Comics.Where(c => c.IsActive);
			comics.ForEach(AddBot);
			//Comics = new BindingList<Comic>(comics.ToList());
		}

		/// <summary>
		/// Adds a new comic to the tasks list.
		/// </summary>
		/// <returns>True if the comic has been added successfully, false otherwise.</returns>
		public void Add(Comic comic)
		{
			AddBot(comic);
			//Comics.Add(comic);
		}

		public void StartAllBots()
		{
			return;
			workers.ForEach(x => x.Run());
		}

		/// <summary>
		/// Stops the specified comic's download.
		/// </summary>
		/// <param name="comic"></param>
		public void Delete(Comic comic)
		{
			return;
			//var bot = bots[comic];
			//if (comic.Status == TaskStatus.Running)
			//    bot.StopDownload();

			//Comics.Remove(comic);

			//bot.DownloadComicCompleted -= DownloadComicCompletedCallback;
			//bot.DownloadCompleted -= DownloadComicsCompletedCallback;
			//bots.Remove(comic);
		}

		/// <summary>
		/// Pauses/unpauses a comic, depending on its current state.
		/// </summary>
		public void Toggle(Comic comic)
		{
			return;
			switch (comic.Status)
			{
				case TaskStatus.Stopped:
					Resume(comic);
					break;
				case TaskStatus.Running:
					Pause(comic);
					break;
			}
		}

		public void Pause(Comic comic)
		{
			return;
			//if (comic.Status != TaskStatus.Running)
			//    return;
			
			//comic.Status = TaskStatus.Stopped;
			//comicRepository.PersistComics();

			//var bot = bots[comic];
			//bot.StopDownload();
		}

		public void Resume(Comic comic)
		{
			//comic.Status = TaskStatus.Running;
			//comicRepository.PersistComics();

			//var bot = bots[comic];
			//if (string.IsNullOrEmpty(comic.CurrentUrl))
			//    bot.DownloadComicsAsync();
			//else
			//    bot.DownloadComicsAsync(comic.CurrentUrl);
		}

		private void AddBot(Comic comic)
		{
			//var bot = new Bot(comic);
			//bot.DownloadComicCompleted += DownloadComicCompletedCallback;
			//bot.DownloadCompleted += DownloadComicsCompletedCallback;

			//bots.Add(comic, bot);
		}

		private void DownloadComicCompletedCallback(object sender, DownloadStripCompletedEventArgs e)
		{
			//synchronizationContext.Post(o =>
			//                                {
			//                                    var comic = ((Bot)sender).Comic;
			//                                    if (!bots.ContainsKey(comic)) //in case the comic has already been removed.
			//                                        return;

			//                                    comic.DownloadedComics++;
			//                                    comic.CurrentUrl = e.CurrentUrl;
			//                                    comicRepository.PersistComics();

			//                                    ResetComicsBindings();
			//                                }, null);
		}

		private void DownloadComicsCompletedCallback(object sender, DownloadCompletedEventArgs e)
		{
			//synchronizationContext.Post(o =>
			//                                {
			//                                    var comic = ((Bot)sender).Comic;
			//                                    if (!bots.ContainsKey(comic)) //in case the comic has already been removed.
			//                                        return;

			//                                    comic.Status = e.DownloadOutcome == DownloadOutcome.Cancelled
			//                                                    ? TaskStatus.Stopped
			//                                                    : TaskStatus.Finished;

			//                                    //only set the currentUrl to null if the outcome is successful
			//                                    if (e.DownloadOutcome == DownloadOutcome.Successful)
			//                                        comic.CurrentUrl = null;

			//                                    comic.DownloadOutcome = e.DownloadOutcome;
			//                                    comicRepository.PersistComics();

			//                                    ResetComicsBindings();

			//                                    if (!UserSettingsOld.CloseWhenAllComicsHaveFinished)
			//                                        return;

			//                                    var allTasksHaveFinished = true;
			//                                    foreach (var _task in Comics)
			//                                    {
			//                                        if (_task.Status != TaskStatus.Running)
			//                                            continue;

			//                                        allTasksHaveFinished = false;
			//                                        break;
			//                                    }

			//                                    if (allTasksHaveFinished)
			//                                        Application.Exit();
			//                                }, null);
		}
	}
}