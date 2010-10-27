using System;
using System.Collections.Generic;
using Woofy.Core.ComicManagement;
using Woofy.Core.Infrastructure;
using MoreLinq;

namespace Woofy.Core
{
	public interface IWorkerSupervisor
	{
	}

	public class WorkerSupervisor : IWorkerSupervisor, ICommandHandler<StartAllDownloads>
	{
	    private readonly IList<Comic> comics;

	    public WorkerSupervisor(IComicStore comicStore)
		{
			comics = comicStore.GetActiveComics();
		}

        #region OBSOLETE

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
        #endregion

	    public void Handle(StartAllDownloads command)
	    {
            comics.ForEach(c => c.Definition.Run());
	    }
	}

    public class StartAllDownloads
    {
    }
}