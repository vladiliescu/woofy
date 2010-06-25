using System.Collections.Generic;

namespace Woofy.Gui.ComicSelection
{
	public class ComicSelectionViewModel
	{
		public IList<ComicDto> AvailableComics { get; set; }
		public IList<ComicDto> ActiveComics { get; set; }

		public ComicSelectionViewModel(IList<ComicDto> availableComics, IList<ComicDto> activeComics)
		{
			AvailableComics = availableComics;
			ActiveComics = activeComics;
		}
	}
}