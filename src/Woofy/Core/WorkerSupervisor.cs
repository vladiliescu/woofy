using System.Collections.Generic;
using System.Threading;
using Woofy.Core.ComicManagement;
using Woofy.Core.Infrastructure;
using MoreLinq;

namespace Woofy.Core
{
    public class StartAllDownloads : ICommand
    {
    }

	public class WorkerSupervisor : ICommandHandler<StartAllDownloads>
	{
	    private readonly IList<Comic> comics;

	    public WorkerSupervisor(IComicStore comicStore)
		{
			comics = comicStore.GetActiveComics();
		}

	    public void Handle(StartAllDownloads command)
	    {
            comics.ForEach(c => ThreadPool.QueueUserWorkItem(o => c.Definition.Run()));
	    }
	}
}