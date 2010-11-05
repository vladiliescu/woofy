using System.Collections.Generic;
using System.Threading;
using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;
using MoreLinq;
using System.Linq;
using Woofy.Flows.AddComic;

namespace Woofy.Flows.Main
{
    public class StartAllDownloads : ICommand
    {
    }

    public class WorkerSupervisor : 
		ICommandHandler<StartAllDownloads>, 
		ICommandHandler<StartDownload>, 
		ICommandHandler<PauseDownload>,
		IEventHandler<ComicActivated>
    {
        private readonly IList<Comic> comics;

        public WorkerSupervisor(IComicStore comicStore)
        {
            comics = comicStore.GetActiveComics();
        }

        public void Handle(StartAllDownloads command)
        {
            comics
                .Where(c => c.Status != WorkerStatus.Paused)
                .ForEach(Start);
        }

        public void Handle(StartDownload command)
        {
            Start(command.Comic);
        }

        public void Handle(PauseDownload command)
        {
			command.Comic.Definition.Stop();
        }

    	public void Handle(ComicActivated eventData)
    	{
			Start(eventData.Comic);
    	}

		private static void Start(Comic comic)
		{
			ThreadPool.QueueUserWorkItem(o => comic.Definition.Run());
		}
    }
}