using Woofy.Core;

namespace Woofy.Gui.ComicSelection
{
	public class DefinitionDto
	{
		public string ComicName { get; private set; }
		public string DefinitionFile { get; private set; }

		public DefinitionDto(ComicDefinition definition)
		{
			ComicName = definition.Name;
			DefinitionFile = definition.Filename;
		}
	}
}