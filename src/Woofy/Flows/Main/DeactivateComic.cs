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
		private readonly IApplicationController appController;

		public DeactivateComicHandler(IApplicationController appController)
		{
			this.appController = appController;
		}

		public void Handle(DeactivateComic command)
		{
			var comic = command.Comic;

			comic.IsActive = false;

			appController.Execute(new PauseDownload(comic));
			appController.Raise(new ComicChanged(comic));
			appController.Raise(new ComicRemoved(comic));
		}
	}
}