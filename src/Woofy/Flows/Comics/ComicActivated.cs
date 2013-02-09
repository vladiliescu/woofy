using Woofy.Core.ComicManagement;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows.Comics
{
	public class ComicActivated : IEvent
	{
		public Comic Comic { get; private set; }

		public ComicActivated(Comic comic)
		{
			Comic = comic;
		}
	}
}