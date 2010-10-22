using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Woofy.Core;
using System.Linq;

namespace Woofy.Gui.ComicSelection
{
	public interface IComicSelectionController
	{
		ComicSelectionViewModel LoadComics();
		void UpdateActiveComics(ComicSelectionInputModel inputModel);
		DialogResult DisplayComicSelectionForm();
	}

	public class ComicSelectionController : IComicSelectionController
	{
		readonly IComicRepository comicRepository;

	    public ComicSelectionController(IComicRepository comicRepository)
		{
			this.comicRepository = comicRepository;
		}

		public ComicSelectionViewModel LoadComics()
		{
			var comics = comicRepository.RetrieveAllComics();
			var availableComics = new List<ComicDto>();
			var activeComics = new List<ComicDto>();

			foreach (var comic in comics)
			{
				if (comic.IsActive)
					activeComics.Add(new ComicDto(comic));
				else
					availableComics.Add(new ComicDto(comic));
			}

			return new ComicSelectionViewModel(availableComics, activeComics);
		}

		public void UpdateActiveComics(ComicSelectionInputModel inputModel)
		{
            foreach (var comic in comicRepository.RetrieveAllComics())
			{
                var comicShouldBeActive = inputModel.ActiveComicDefinitions.SingleOrDefault(def => def == comic.DefinitionId) != null;
                comic.IsActive = comicShouldBeActive;
			}
			
			comicRepository.PersistComics();
		}

		public DialogResult DisplayComicSelectionForm()
		{
			using (var form = new ComicSelectionForm(this))
			{
				return form.ShowDialog();
			}
		}
	}
}