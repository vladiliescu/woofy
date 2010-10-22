using System;
using System.Collections.Generic;

namespace Woofy.Gui.ComicSelection
{
	public class ComicSelectionInputModel
	{
		public IList<string> ActiveComicDefinitions { get; private set; }

		public ComicSelectionInputModel(IList<string> activeDefinitionFiles)
		{
			ActiveComicDefinitions = activeDefinitionFiles;
		}
	}
}