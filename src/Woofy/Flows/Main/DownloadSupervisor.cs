using System.Threading;
using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;
using MoreLinq;
using System.Linq;
using Woofy.Flows.Comics;

namespace Woofy.Flows.Main
{
	public class DownloadSupervisor : 
		ICommandHandler<StartAllDownloads>, 
		ICommandHandler<StartDownload>, 
		ICommandHandler<PauseDownload>,
		IEventHandler<ComicActivated>
    {
		private readonly IComicStore comicStore;
		

        public DownloadSupervisor(IComicStore comicStore)
        {
        	this.comicStore = comicStore;
        	
        }

		public void Handle(StartAllDownloads command)
        {
            comicStore.GetActiveComics()
                .Where(c => c.Status != Status.Paused)
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
			var comic = eventData.Comic;
			if (comic.Status == Status.Running)
				Start(eventData.Comic);
    	}

		private static void Start(Comic comic)
		{
            new Thread(() => comic.Definition.Run());
		}
    }
}