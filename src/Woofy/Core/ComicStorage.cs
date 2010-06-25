using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace Woofy.Core
{
	public interface IComicStorage
	{
		void Add(Comic comic);
		void Update(Comic comic);
		void Delete(Comic comic);

		IList<Comic> RetrieveActiveComics();
		void ReplaceWith(IList<Comic> comics);
		IList<Comic> RetrieveAllComics();
	}

	public class ComicStorage : IComicStorage
	{
		IList<Comic> comicsCache;
		readonly IAppSettings appSettings;
		readonly IDefinitionStorage definitionStorage;

		public ComicStorage(IAppSettings appSettings, IDefinitionStorage definitionStorage)
		{
			this.appSettings = appSettings;
			this.definitionStorage = definitionStorage;

			InitializeComicsCache();
		}

		private void InitializeComicsCache()
		{
			comicsCache = new List<Comic>();

			EnsureFileExists(appSettings.ComicsFile);
			var comics = ReadSerializedComics();
			var definitions = definitionStorage.RetrieveAll();

			foreach (var comic in comics)
			{
				var comicIsSerializedAndHasADefinition = definitions.FirstOrDefault(x => x.Filename == comic.DefinitionFilename) != null;
				if (!comicIsSerializedAndHasADefinition)
					continue;
				
				comicsCache.Add(comic);
			}

			foreach (var definition in definitions)
			{
				var comicIsNotSerializedAndHasADefinition = comics.FirstOrDefault(x => x.DefinitionFilename == definition.Filename) == null;
				if (!comicIsNotSerializedAndHasADefinition)
					continue;

				comicsCache.Add(new Comic(definition));
			}

			PersistComics();
		}

		private IList<Comic> ReadSerializedComics()
		{
			var json = File.ReadAllText(appSettings.ComicsFile);
			var comics = JsonConvert.DeserializeObject<List<Comic>>(json) ?? new List<Comic>();
			comics.ForEach(x => x.Definition = x.DefinitionFilename != null ? definitionStorage.Retrieve(x.DefinitionFilename) : null);
			return comics;
		}

		public void Add(Comic comic)
		{
			comicsCache.Add(comic);
			PersistComics();
		}

		public void Update(Comic comic)
		{
			PersistComics();
		}

		public void Delete(Comic comic)
		{
			comicsCache.Remove(comic);
			PersistComics();
		}

		public void ReplaceWith(IList<Comic> comics)
		{
			comicsCache = comics;
			PersistComics();
		}

		public IList<Comic> RetrieveAllComics()
		{
			throw new NotImplementedException();
		}

		public IList<Comic> RetrieveActiveComics()
		{
			return new List<Comic>(comicsCache);
		}

		private void PersistComics()
		{
			File.WriteAllText(appSettings.ComicsFile, JsonConvert.SerializeObject(comicsCache, Formatting.Indented));
		}

		private static void EnsureFileExists(string file)
		{
			if (File.Exists(file))
				return;

			File.Create(file).Close();
		}
	}
}