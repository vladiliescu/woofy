using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows.Download
{
    public class CurrentPageChangedHandler : IEventHandler<CurrentPageChanged>
    {
        private readonly IComicStore comicStore;
        private readonly IApplicationController applicationController;

        public CurrentPageChangedHandler(IComicStore comicStore, IApplicationController applicationController)
        {
            this.comicStore = comicStore;
            this.applicationController = applicationController;
        }

        public void Handle(CurrentPageChanged eventData)
        {
#warning race condition (although there should be at most one thread updating any given comic).
            var comic = comicStore.Find(eventData.ComicId);
            comic.CurrentPage = eventData.Url;

            applicationController.Raise(new ComicChanged(comic));
        }
    }
}