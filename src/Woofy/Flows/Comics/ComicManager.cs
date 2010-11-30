using Woofy.Core;
using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows.Comics
{
    public class ComicManager : ICommandHandler<AddComic>, IEventHandler<ComicAdded>
    {
        private readonly IComicStore comicStore;
        private readonly IApplicationController applicationController;
        private readonly IScreenActivator screenActivator;

        public ComicManager(IComicStore comicStore, IApplicationController applicationController, IScreenActivator screenActivator)
        {
            this.screenActivator = screenActivator;
            this.applicationController = applicationController;
            this.comicStore = comicStore;
        }

        public void Handle(AddComic command)
        {
            screenActivator.AddComic();
        }

        public void Handle(ComicAdded eventData)
        {
            var comicId = eventData.ComicId;
            var comic = comicStore.Find(comicId);
            comic.Status = Status.Running;

            comicStore.PersistComics();
            applicationController.Raise(new ComicActivated(comic));
        }
    }
}