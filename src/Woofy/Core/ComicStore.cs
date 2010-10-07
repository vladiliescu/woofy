using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace Woofy.Core
{
	public interface IComicStore
	{
        Comic[] Comics { get; }
	    void PersistComics();
	    Comic FindByFilename(string definitionFilename);
	}

	public class ComicStore : IComicStore
	{
        public Comic[] Comics { get; private set; }
		readonly IAppSettings appSettings;
		readonly IDefinitionStore definitionStore;

		public ComicStore(IAppSettings appSettings, IDefinitionStore definitionStore)
		{
			this.appSettings = appSettings;
			this.definitionStore = definitionStore;

			InitializeComicCache();
		}

		private void InitializeComicCache()
		{
			EnsureFileExists(appSettings.ComicsFile);
			var definitions = definitionStore.Definitions;

            var comics = new List<Comic>();
			foreach (var comic in ReadSerializedComics())
			{
				var comicIsSerializedAndHasADefinition = definitions.FirstOrDefault(x => x.Id == comic.DefinitionId) != null;
				if (!comicIsSerializedAndHasADefinition)
					continue;
				
				comics.Add(comic);
			}

			foreach (var definition in definitions)
			{
				var comicIsNotSerializedAndHasADefinition = comics.FirstOrDefault(x => x.DefinitionId == definition.Id) == null;
				if (!comicIsNotSerializedAndHasADefinition)
					continue;

				comics.Add(new Comic(definition));
			}
            Comics = comics.ToArray();

			PersistComics();
		}

		private IEnumerable<Comic> ReadSerializedComics()
		{
			var json = File.ReadAllText(appSettings.ComicsFile);
			var comics = JsonConvert.DeserializeObject<List<Comic>>(json) ?? new List<Comic>();
			comics.ForEach(x => x.Definition = x.DefinitionId != null ? definitionStore.FindByFilename(x.DefinitionId) : null);
			return comics;
		}

		public void PersistComics()
		{
			File.WriteAllText(appSettings.ComicsFile, JsonConvert.SerializeObject(Comics, Formatting.Indented));
		}

        public Comic FindByFilename(string definitionFilename)
        {
            return Comics.SingleOrDefault(x => x.DefinitionId == definitionFilename);
        }

	    private static void EnsureFileExists(string file)
		{
			if (File.Exists(file))
				return;

			File.Create(file).Close();
		}
	}
}