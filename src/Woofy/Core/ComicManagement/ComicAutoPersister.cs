using System;
using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;
using Woofy.Flows.Download;

namespace Woofy.Core.ComicManagement
{
    public class ComicAutoPersister : IEventHandler<ComicChanged>
    {
        private readonly IComicStore comicStore;

        public ComicAutoPersister(IComicStore comicStore)
        {
            this.comicStore = comicStore;
        }

        public void Handle(ComicChanged eventData)
        {
#warning not thread-safe
            comicStore.PersistComics();
        }
    }
}