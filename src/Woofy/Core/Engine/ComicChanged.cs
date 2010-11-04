using Woofy.Core.ComicManagement;
using Woofy.Core.Infrastructure;

namespace Woofy.Core.Engine
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