using Woofy.Core.ComicManagement;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows.Main
{
    public class PauseDownload : ICommand
    {
        public Comic Comic { get; private set; }

        public PauseDownload(Comic comic)
        {
            Comic = comic;
        }
    }
}