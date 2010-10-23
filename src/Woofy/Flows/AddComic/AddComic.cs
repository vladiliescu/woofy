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
		private readonly IComicActivator detailsPresenter;
		private readonly IComicStore comicStore;

		public AddComicHandler(IComicActivator detailsPresenter, IComicStore comicStore)
		{
			this.detailsPresenter = detailsPresenter;
			this.comicStore = comicStore;
		}

		public void Handle(AddComic command)
		{
			var result = detailsPresenter.Activate();
			if (result.ServiceResult != ServiceResult.Ok)
				return;

			var comicId = result.Data;
			var comic = comicStore.Find(comicId);
			comic.IsActive = true;
			comicStore.PersistComics();
		}
	}
}