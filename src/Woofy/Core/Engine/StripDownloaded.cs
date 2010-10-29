using Woofy.Core.Infrastructure;

namespace Woofy.Core.Engine
{
    public class StripDownloaded : IEvent
    {
        public string ComicId { get; private set; }

        public StripDownloaded(string comicId)
        {
            ComicId = comicId;
        }
    }
}