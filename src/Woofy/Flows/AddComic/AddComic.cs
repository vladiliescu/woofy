using Woofy.Core;
using Woofy.Core.ComicManagement;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows.AddComic
{
	public class AddComic
	{
		
	}

	public class AddComicHandler : ICommandHandler<AddComic>
	{
		private readonly IComicActivator comicActivator;
		private readonly IComicStore comicStore;
		private readonly IApplicationController applicationController;
		readonly IWorkerSupervisor w;

		public AddComicHandler(IComicActivator comicActivator, IComicStore comicStore, IApplicationController applicationController, IWorkerSupervisor w)
		{
			this.comicActivator = comicActivator;
			this.w = w;
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