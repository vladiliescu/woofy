using Woofy.Core;
using Woofy.Core.ComicManagement;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows.AddComic
{
	public class AddComic : ICommand
	{
		
	}

	public class AddComicHandler : ICommandHandler<AddComic>
	{
		private readonly IComicActivator comicActivator;
		private readonly IComicStore comicStore;
		private readonly IApplicationController applicationController;

		public AddComicHandler(IComicActivator comicActivator, IComicStore comicStore, IApplicationController applicationController)
		{
			this.comicActivator = comicActivator;
			this.applicationController = applicationController;
			this.comicStore = comicStore;
		}

		public void Handle(AddComic command)
		{
			var result = comicActivator.Activate();
			if (result.ServiceResult != ServiceResult.Ok)
				return;

			var comicId = result.Data;
			var comic = comicStore.Find(comicId);
			comic.IsActive = true;
			comicStore.PersistComics();

			applicationController.Raise(new ComicActivated(comic));
		}
	}
}