using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows.Download
{
    public class StripDownloadedHandler : IEventHandler<StripDownloaded>
    {
        private readonly IComicStore comicStore;
        private readonly IAppController appController;

        public StripDownloadedHandler(IComicStore comicStore, IAppController appController)
        {
            this.comicStore = comicStore;
            this.appController = appController;
        }

        public void Handle(StripDownloaded eventData)
        {
#warning race condition
            var comic = comicStore.Find(eventData.ComicId);
            comic.DownloadedStrips++;

            appController.Raise(new ComicChanged(comic));
        }
    }
}