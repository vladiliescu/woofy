using System.Collections.Generic;

namespace Woofy.Gui.ComicSelection
{
	public class ComicSelectionViewModel
	{
		public IList<DefinitionDto> AvailableComics { get; set; }
		public IList<DefinitionDto> ActiveComics { get; set; }

		public ComicSelectionViewModel(IList<DefinitionDto> availableComics, IList<DefinitionDto> activeComics)
		{
			AvailableComics = availableComics;
			ActiveComics = activeComics;
		}
	}
}