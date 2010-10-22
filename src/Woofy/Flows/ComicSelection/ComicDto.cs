using Woofy.Core;

namespace Woofy.Gui.ComicSelection
{
	public class ComicDto
	{
		public string ComicName { get; private set; }
		public string DefinitionFile { get; private set; }

		public ComicDto(Comic comic)
		{
			ComicName = comic.Name;
			DefinitionFile = comic.DefinitionId;
		}
	}
}