using Woofy.Core.ComicManagement;
using Woofy.Core.Infrastructure;

namespace Woofy.Core.Engine
{
	public class DownloadFinished : IEvent
	{
		public string ComicId { get; private set; }

		public DownloadFinished(string comicId)
		{
			ComicId = comicId;
		}
	}

	public class DownloadFinishedHandler : IEventHandler<DownloadFinished>
	{
		private readonly IAppController appController;
		private readonly IComicStore comicStore;

		public DownloadFinishedHandler(IAppController appController, IComicStore comicStore)
		{
			this.appController = appController;
			this.comicStore = comicStore;
		}

		public void Handle(DownloadFinished eventData)
		{
			var comic = comicStore.Find(eventData.ComicId);
			comic.HasFinished = true;

			appController.Raise(new ComicChanged(comic));
		}
	}
}