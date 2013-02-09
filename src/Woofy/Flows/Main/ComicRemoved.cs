using Woofy.Core.ComicManagement;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows.Main
{
	public class ComicRemoved : IEvent
	{
		public Comic Comic { get; private set; }

		public ComicRemoved(Comic comic)
		{
			Comic = comic;
		}
	}
}