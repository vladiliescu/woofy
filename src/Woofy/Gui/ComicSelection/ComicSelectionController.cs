using System.Collections.Generic;
using Woofy.Core;
using System.Linq;

namespace Woofy.Gui.ComicSelection
{
	public interface IComicSelectionController
	{
		ComicSelectionViewModel LoadComics();
		void UpdateActiveComics(ComicSelectionInputModel inputModel);
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
			var comics = comicStorage.RetrieveAll();
			var definitions = definitionStorage.RetrieveAll();
			var availableComics = new List<DefinitionDto>();
			var activeComics = new List<DefinitionDto>();

			foreach (var definition in definitions)
			{
				var definitionIsActive = comics.FirstOrDefault(x => x.Definition == definition) != null;
				if (definitionIsActive)
					activeComics.Add(new DefinitionDto(definition));
				else
					availableComics.Add(new DefinitionDto(definition));
			}

			return new ComicSelectionViewModel(availableComics, activeComics);
		}

		public void UpdateActiveComics(ComicSelectionInputModel inputModel)
		{
			var comics = new List<Comic>();
			foreach (var definitionFile in inputModel.ActiveComicDefinitions)
			{
				//TODO: I could optimize this by maintaining a definition cache similar to the Comics one; this means that the definitions will not be reloaded each time though
				comics.Add(new Comic(new ComicDefinition(definitionFile)));
			}

			comicStorage.ReplaceWith(comics);
		}
	}
}