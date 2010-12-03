using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;
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
#warning race condition
            var comic = comicStore.Find(eventData.ComicId);
            comic.DownloadedStrips++;

            applicationController.Raise(new ComicChanged(comic));
        }
    }
}