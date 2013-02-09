using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows.Main
{
	public class DeactivateComic : ICommand
	{
		public Comic Comic { get; private set; }

		public DeactivateComic(Comic comic)
		{
			Comic = comic;
		}
	}

	public class DeactivateComicHandler : ICommandHandler<DeactivateComic>
	{
		private readonly IAppController appController;

		public DeactivateComicHandler(IAppController appController)
		{
			this.appController = appController;
		}

		public void Handle(DeactivateComic command)
		{
			var comic = command.Comic;

            appController.Execute(new PauseDownload(comic));

			comic.Status = Status.Inactive;
            comic.CurrentPage = null;
            comic.DownloadedStrips = 0;
            comic.HasFinished = false;
			
			appController.Raise(new ComicChanged(comic));
			appController.Raise(new ComicRemoved(comic));
		}
	}
}