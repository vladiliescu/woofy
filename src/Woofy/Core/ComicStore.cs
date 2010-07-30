using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace Woofy.Core
{
	public interface IComicStore
	{
        Comic[] Comics { get; }
		void Add(Comic comic);
		void Update(Comic comic);
		void Delete(Comic comic);
	}

	public class ComicStore : IComicStore
	{
        private readonly IList<Comic> comics = new List<Comic>();
        public Comic[] Comics { get; private set; }
		readonly IAppSettings appSettings;
		readonly IDefinitionStore definitionStore;

		public ComicStore(IAppSettings appSettings, IDefinitionStore definitionStore)
		{
			this.appSettings = appSettings;
			this.definitionStore = definitionStore;

			InitializeComicsCache();
		}

		private void InitializeComicsCache()
		{
			EnsureFileExists(appSettings.ComicsFile);
			var definitions = definitionStore.RetrieveAll();

			foreach (var comic in ReadSerializedComics())
			{
				var comicIsSerializedAndHasADefinition = definitions.FirstOrDefault(x => x.Filename == comic.DefinitionFilename) != null;
				if (!comicIsSerializedAndHasADefinition)
					continue;
				
				comics.Add(comic);
			}

			foreach (var definition in definitions)
			{
				var comicIsNotSerializedAndHasADefinition = comics.FirstOrDefault(x => x.DefinitionFilename == definition.Filename) == null;
				if (!comicIsNotSerializedAndHasADefinition)
					continue;

				comics.Add(new Comic(definition));
			}

			PersistComics();
		}

		private IEnumerable<Comic> ReadSerializedComics()
		{
			var json = File.ReadAllText(appSettings.ComicsFile);
			var comics = JsonConvert.DeserializeObject<List<Comic>>(json) ?? new List<Comic>();
			comics.ForEach(x => x.Definition = x.DefinitionFilename != null ? definitionStore.Retrieve(x.DefinitionFilename) : null);
			return comics;
		}

	    public void Add(Comic comic)
		{
			comics.Add(comic);
			PersistComics();
		}

		public void Update(Comic comic)
		{
			PersistComics();
		}

		public void Delete(Comic comic)
		{
			comics.Remove(comic);
			PersistComics();
		}

		private void PersistComics()
		{
            //everytime someone modifies the comics list I persist it, and also update the cached array
            Comics = comics.ToArray();
			File.WriteAllText(appSettings.ComicsFile, JsonConvert.SerializeObject(comics, Formatting.Indented));
		}

		private static void EnsureFileExists(string file)
		{
			if (File.Exists(file))
				return;

			File.Create(file).Close();
		}
	}
}