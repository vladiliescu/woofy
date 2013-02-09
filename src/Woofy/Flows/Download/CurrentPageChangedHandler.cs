using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows.Download
{
    public class CurrentPageChangedHandler : IEventHandler<CurrentPageChanged>
    {
        private readonly IComicStore comicStore;
        private readonly IAppController appController;

        public CurrentPageChangedHandler(IComicStore comicStore, IAppController appController)
        {
            this.comicStore = comicStore;
            this.appController = appController;
        }

        public void Handle(CurrentPageChanged eventData)
        {
#warning race condition (although there should be at most one thread updating any given comic).
            var comic = comicStore.Find(eventData.ComicId);
            comic.CurrentPage = eventData.Url;

            appController.Raise(new ComicChanged(comic));
        }
    }
}