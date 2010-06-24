using System.Collections.Generic;
using Woofy.Core;
using System.Linq;

namespace Woofy.Gui.ComicSelection
{
	public interface IComicSelectionController
	{
		ComicSelectionViewModel LoadComics();
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
	}
}