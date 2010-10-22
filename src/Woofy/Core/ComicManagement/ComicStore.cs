using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using Woofy.Core.Engine;
using Woofy.Core.SystemProxies;

namespace Woofy.Core.ComicManagement
{
	public interface IComicStore
	{
        Comic[] Comics { get; }
	    void PersistComics();
	    Comic FindByFilename(string definitionFilename);
	    void InitializeComicCache();
	}

	public class ComicStore : IComicStore
	{
        public Comic[] Comics { get; private set; }
		private readonly IAppSettings appSettings;
		private readonly IDefinitionStore definitionStore;
        private readonly IFileProxy file;
        private readonly IUserSettings userSettings;

		public ComicStore(IAppSettings appSettings, IDefinitionStore definitionStore, IFileProxy file, IUserSettings userSettings)
		{
			this.appSettings = appSettings;
		    this.userSettings = userSettings;
		    this.file = file;
		    this.definitionStore = definitionStore;
		}

		public void InitializeComicCache()
		{
			//EnsureFileExists(appSettings.ComicsFile);
            var comics = new List<Comic>();
            var serializedComics = ReadSerializedComics();
		    foreach (var definition in definitionStore.Definitions)
		    {
                var associatedComic = serializedComics.SingleOrDefault(x => x.DefinitionId == definition.Id);
                if (associatedComic != null)
                    associatedComic.Definition = definition;
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
			    Definition = definition,
			    DefinitionId = definition.Id,
                DownloadFolder = userSettings.DefaultDownloadFolder.IsNotNullOrEmpty() ? Path.Combine(userSettings.DefaultDownloadFolder, definition.Id) : definition.Id,
			    Status = TaskStatus.Running
            };

            return comic;
	    }

	    private IEnumerable<Comic> ReadSerializedComics()
		{
			var json = file.ReadAllText(appSettings.ComicsFile);
			var comics = JsonConvert.DeserializeObject<Comic[]>(json) ?? new Comic[0];
			return comics;
		}

		public void PersistComics()
		{
			file.WriteAllText(appSettings.ComicsFile, JsonConvert.SerializeObject(Comics, Formatting.Indented));
		}

        public Comic FindByFilename(string definitionFilename)
        {
            return Comics.SingleOrDefault(x => x.DefinitionId == definitionFilename);
        }

#warning this should be handled by a startup task
	    private static void EnsureFileExists(string file)
		{
			if (File.Exists(file))
				return;

			File.Create(file).Close();
		}
	}
}