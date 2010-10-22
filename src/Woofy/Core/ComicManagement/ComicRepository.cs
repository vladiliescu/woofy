using System.Linq;

namespace Woofy.Core.ComicManagement
{
    public interface IComicRepository
    {
        Comic[] RetrieveActiveComics();
        Comic[] RetrieveAllComics();
        Comic Retrieve(string definitionFilename);
        void PersistComics();
    }

    public class ComicRepository : IComicRepository
    {
        readonly IComicStore comicStore;

        public ComicRepository(IComicStore comicStore)
        {
            this.comicStore = comicStore;
        }

        public Comic[] RetrieveActiveComics()
        {
            return comicStore.Comics.Where(x => x.IsActive).ToArray();
        }

        public Comic[] RetrieveAllComics()
        {
            return comicStore.Comics;
        }

        public Comic Retrieve(string definitionFilename)
        {
            return comicStore.Comics.Where(x => x.DefinitionId == definitionFilename).SingleOrDefault();
        }

        public void PersistComics()
        {
            comicStore.PersistComics();
        }
    }
}