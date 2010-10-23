using Woofy.Core.ComicManagement;

namespace Woofy.Flows.AddComic
{
	public class ComicActivated
	{
		public Comic Comic { get; private set; }

		public ComicActivated(Comic comic)
		{
			Comic = comic;
		}
	}
}