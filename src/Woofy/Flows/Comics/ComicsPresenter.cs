using Woofy.Core;
using Woofy.Core.ComicManagement;
using System.Linq;
using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows.Comics
{
	public interface IComicsPresenter
	{
        AddViewModel Load();
	    void SelectComic(AddInputModel inputModel);
	}

	public class ComicsPresenter : IComicsPresenter, ICommandHandler<AddComic>
	{
		private readonly IComicStore comicStore;
        private readonly IApplicationController appController;
        private readonly IScreenActivator screenActivator;

		public ComicsPresenter(IComicStore comicStore, IApplicationController appController, IScreenActivator screenActivator)
		{
		    this.comicStore = comicStore;
		    this.screenActivator = screenActivator;
		    this.appController = appController;
		}

		public AddViewModel Load()
		{
			return new AddViewModel(
				comicStore.GetInactiveComics()
					.Select(comic => new AddViewModel.ComicModel(comic.Id, comic.Name))
					.ToArray()
				);
		}

	    public void SelectComic(AddInputModel inputModel)
	    {
            var comicId = inputModel.ComicId;
            var comic = comicStore.Find(comicId);
            
            comic.Status = Status.Running;
            comic.PrependIndexToStrips = inputModel.PrependIndexToStrips;

            comicStore.PersistComics();
            appController.Raise(new ComicActivated(comic));
	    }

	    public void Handle(AddComic command)
	    {
            screenActivator.AddComic();
	    }
	}
}