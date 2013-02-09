using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using Woofy.Core.Engine;
using Woofy.Core.SystemProxies;

namespace Woofy.Core.ComicManagement
{
	public interface IComicStore
	{
        Comic[] Comics { get; }
        Comic[] GetActiveComics();
		Comic[] GetInactiveComics();
	    void PersistComics();
	    Comic Find(string id);
	    void InitializeComicCache();
	}

	public class ComicStore : IComicStore
	{
        public Comic[] Comics { get; private set; }
		
		private readonly IAppSettings appSettings;
		private readonly IDefinitionStore definitionStore;
        private readonly IFileProxy file;
		private readonly object writeLock = new object();

		public ComicStore(IAppSettings appSettings, IDefinitionStore definitionStore, IFileProxy file)
		{
			this.appSettings = appSettings;
		    this.file = file;
		    this.definitionStore = definitionStore;
		}

		public void InitializeComicCache()
		{
            var comics = new List<Comic>();
            var serializedComics = ReadSerializedComics();
		    foreach (var definition in definitionStore.Definitions)
		    {
                var associatedComic = serializedComics.SingleOrDefault(x => x.Id == definition.Id);
                if (associatedComic != null)
                    associatedComic.SetDefinition(definition);
                else
                    associatedComic = CreateComicFor(definition);
                
                comics.Add(associatedComic);
		    }
            
            Comics = comics.ToArray();
			PersistComics();
		}

	    private Comic CreateComicFor(Definition definition)
	    {
            var comic = new Comic
            {
                Name = definition.Comic,
			    Id = definition.Id,
			    Status = Status.Inactive
            };

            comic.SetDefinition(definition);

            return comic;
	    }

	    private IEnumerable<Comic> ReadSerializedComics()
		{
			var json = file.Exists(appSettings.ComicsFile) ?
				file.ReadAllText(appSettings.ComicsFile) :
				"";

			var comics = JsonConvert.DeserializeObject<Comic[]>(json) ?? new Comic[0];
			return comics;
		}

	    public Comic[] GetActiveComics()
	    {
            return Comics.Where(x => x.Status != Status.Inactive).ToArray();
	    }

		public Comic[] GetInactiveComics()
		{
			return Comics.Where(x => x.Status == Status.Inactive).ToArray();
		}

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Thread-safe.
        /// </remarks>
		public void PersistComics()
		{
	    	lock (writeLock)
	    	{
				file.WriteAllText(appSettings.ComicsFile, JsonConvert.SerializeObject(Comics, Formatting.Indented));
	    	}
		}

        public Comic Find(string id)
        {
            return Comics.SingleOrDefault(x => x.Id == id);
        }
	}
}