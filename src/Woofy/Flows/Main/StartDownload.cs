using Woofy.Core.ComicManagement;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows.Main
{
	public class StartDownload : ICommand
	{
		public Comic Comic { get; private set; }

		public StartDownload(Comic comic)
		{
			Comic = comic;
		}
	}
}