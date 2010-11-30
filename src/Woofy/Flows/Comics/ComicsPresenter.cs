using Woofy.Core.ComicManagement;
using System.Linq;
using Woofy.Core.Infrastructure;
using Woofy.Flows.AddComic;

namespace Woofy.Flows.Comics
{
	public interface IComicsPresenter
	{
        ComicDetailsViewModel Load();
	    void SelectComic(string id);
	}

	public class ComicsPresenter : IComicsPresenter
	{
		private readonly IComicStore comicStore;
        private readonly IApplicationController appController;

		public ComicsPresenter(IComicStore comicStore, IApplicationController appController)
		{
		    this.comicStore = comicStore;
		    this.appController = appController;
		}

		public ComicDetailsViewModel Load()
		{
			return new ComicDetailsViewModel(
				comicStore.GetInactiveComics()
					.Select(comic => new ComicDetailsViewModel.ComicModel(comic.Id, comic.Name))
					.ToArray()
				);
		}

		public void SelectComic(string id)
		{
            appController.Raise(new ComicAdded(id));
		}
	}
}