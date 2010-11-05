using Woofy.Core.Engine;
using Woofy.Enums;

namespace Woofy.Flows.Main
{
    public class ComicViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int DownloadedStrips { get; set; }
        public WorkerStatus Status { get; set; }
        public string CurrentPage { get; set; }
    }

    public class ComicInputModel
    {
        public string Id { get; set; }
    }
}