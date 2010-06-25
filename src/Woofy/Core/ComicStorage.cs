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

		/// <summary>
		/// Returns a list with all the tasks in the database
		/// </summary>
		/// <returns></returns>
		IList<Comic> RetrieveAll();

		IList<Comic> RetrieveActiveTasksByComicInfoFile(string comicInfoFile);
		void ReplaceWith(IList<Comic> comics);
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

		public void InitializeComicsCache()
		{
			EnsureFileExists(appSettings.ComicsFile);
			var json = File.ReadAllText(appSettings.ComicsFile);
			comicsCache = JsonConvert.DeserializeObject<List<Comic>>(json) ?? new List<Comic>();
			comicsCache.ForEach(x => x.Definition = definitionStorage.Retrieve(x.Definition.Filename));
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

		/// <summary>
		/// Returns a list with all the tasks in the database
		/// </summary>
		/// <returns></returns>
		public IList<Comic> RetrieveAll()
		{
			return new List<Comic>(comicsCache);
		}

		public IList<Comic> RetrieveActiveTasksByComicInfoFile(string comicInfoFile)
		{
			var tasks = new List<Comic>();
			foreach (var task in comicsCache)
			{
				if (task.Definition.Filename == comicInfoFile)
					tasks.Add(task);
			}

			return tasks;
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