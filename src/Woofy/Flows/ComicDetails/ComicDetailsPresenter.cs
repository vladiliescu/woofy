using System.Collections.Generic;
using Woofy.Core;
using Woofy.Core.ComicManagement;
using Woofy.Core.Infrastructure;
using Woofy.Gui.ComicSelection;
using System.Linq;

namespace Woofy.Flows.ComicDetails
{
	public interface IComicDetailsPresenter
	{
	    ComicDetailsViewModel Load();
	}

	public class ComicDetailsPresenter : IComicDetailsPresenter, ICommandHandler<AddComic>
	{
        private readonly IComicRepository comicRepository;
	    public ComicDetailsPresenter(IComicRepository comicRepository)
	    {
	        this.comicRepository = comicRepository;
	    }

	    public void Handle(AddComic command)
		{
			using (var form = new ComicDetailsForm(this))
			{
				form.ShowDialog();
			}
		}

        public ComicDetailsViewModel Load()
        {
            var comics = comicRepository.RetrieveAllComics();
            return new ComicDetailsViewModel(
                comics
                    .Where(comic => !comic.IsActive)
                    .Select(comic => new ComicDetailsViewModel.ComicModel(comic.Name, comic.DownloadFolder))
                    .ToArray()
                );
        }
	}
}