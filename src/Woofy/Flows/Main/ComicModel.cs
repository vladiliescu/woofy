using Woofy.Core.Engine;

namespace Woofy.Flows.Main
{
    public class ComicViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int DownloadedStrips { get; set; }
		public ComicStatus Status { get; set; }
        public string CurrentPage { get; set; }

		public enum ComicStatus
		{
			Paused = 0,
			Running = 1,
			Finished = 2
		}
    }
}