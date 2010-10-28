using System;
using Woofy.Core;
using Woofy.Core.ComicManagement;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows.Download
{
    public class StripDownloadedHandler : IEventHandler<StripDownloaded>
    {
        private readonly IComicStore comicStore;
        private readonly IApplicationController applicationController;

        public StripDownloadedHandler(IComicStore comicStore, IApplicationController applicationController)
        {
            this.comicStore = comicStore;
            this.applicationController = applicationController;
        }

        public void Handle(StripDownloaded eventData)
        {
            var comic = comicStore.Find(eventData.ComicId);
            comic.DownloadedStrips++;

#warning should I create a dedicated event/handler for persisting the comics when they change?
            comicStore.PersistComics();

            applicationController.Raise(new ComicChanged(comic));
        }
    }
}