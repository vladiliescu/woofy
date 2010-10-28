using Woofy.Core.ComicManagement;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows.Download
{
    public class ComicChanged : IEvent
    {
        public Comic Comic { get; private set; }

        public ComicChanged(Comic comic)
        {
            Comic = comic;
        }
    }
}