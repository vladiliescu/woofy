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
		readonly IComicStorage comicStorage;
		readonly IDefinitionStorage definitionStorage;

		public ComicSelectionController(IComicStorage comicStorage, IDefinitionStorage definitionStorage)
		{
			this.comicStorage = comicStorage;
			this.definitionStorage = definitionStorage;
		}

		public ComicSelectionViewModel LoadComics()
		{
			var comics = comicStorage.RetrieveAllComics();
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
			var comics = new List<Comic>();
			foreach (var definitionFile in inputModel.ActiveComicDefinitions)
			{
				//TODO: I could optimize this by maintaining a definition cache similar to the Comics one; this means that the definitions will not be reloaded each time though
				comics.Add(new Comic(definitionStorage.Retrieve(definitionFile)));
			}

			//de schimbat replace-ul: nu vreau sa pierd statisticile de download
			comicStorage.ReplaceWith(comics);
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