using System;
using System.Linq;

namespace Woofy.Core
{
    public interface IComicRepository
    {
        Comic[] RetrieveActiveComics();
        Comic[] RetrieveAllComics();
        Comic Retrieve(string definitionFilename);
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
            return comicStore.Comics.Where(x => x.DefinitionFilename == definitionFilename).SingleOrDefault();
        }
    }
}