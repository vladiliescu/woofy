using Woofy.Core.Infrastructure;

namespace Woofy.Flows.Comics
{
    public class ComicAdded : IEvent
    {
        public string ComicId { get; private set; }

        public ComicAdded(string comicId)
        {
            ComicId = comicId;
        }
    }
}