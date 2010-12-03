using Woofy.Core.ComicManagement;
using System.Linq;
using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows.Comics
{
	public interface IComicsPresenter
	{
        AddViewModel InitializeAdd();
	    void AddComic(AddInputModel inputModel);
	    void EditComic(EditInputModel editInputModel);
	}

	public class ComicsPresenter : IComicsPresenter, ICommandHandler<AddComic>, ICommandHandler<EditComic>
	{
		private readonly IComicStore comicStore;
        private readonly IAppController appController;

		public ComicsPresenter(IComicStore comicStore, IAppController appController)
		{
		    this.comicStore = comicStore;
		    this.appController = appController;
		}

		public AddViewModel InitializeAdd()
		{
			return new AddViewModel(
				comicStore.GetInactiveComics()
					.Select(comic => new AddViewModel.ComicModel(comic.Id, comic.Name))
					.ToArray()
				);
		}

	    public void AddComic(AddInputModel inputModel)
	    {
            var comic = comicStore.Find(inputModel.ComicId);
            
            comic.Status = Status.Running;
            comic.PrependIndexToStrips = inputModel.PrependIndexToStrips;

            comicStore.PersistComics();
            appController.Raise(new ComicActivated(comic));
	    }

	    public void EditComic(EditInputModel inputModel)
	    {
            var comic = comicStore.Find(inputModel.ComicId);
            comic.PrependIndexToStrips = inputModel.PrependIndexToStrips;

            comicStore.PersistComics();
	    }

	    public void Handle(AddComic command)
	    {
            using (var form = new AddForm(this))
            {
                form.ShowDialog();
            }
	    }

	    public void Handle(EditComic command)
	    {
            var comic = comicStore.Find(command.ComicId);
            using (var form = new EditForm(this, new EditViewModel(comic.Id, comic.Name, comic.PrependIndexToStrips)))
            {
                form.ShowDialog();
            }
	    }
	}
}