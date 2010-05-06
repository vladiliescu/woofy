using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Woofy.Core
{
	public interface IComicsStorage
	{
		void Load();
		void Add(ComicTask comic);
		void Update(ComicTask comic);
		void Delete(ComicTask comic);

		/// <summary>
		/// Returns a list with all the tasks in the database
		/// </summary>
		/// <returns></returns>
		IList<ComicTask> RetrieveAllTasks();

		IList<ComicTask> RetrieveActiveTasksByComicInfoFile(string comicInfoFile);
	}

	public class ComicsStorage : IComicsStorage
	{
		IList<ComicTask> taskCache;
		readonly IAppSettings appSettings;

		public ComicsStorage(IAppSettings appSettings)
		{
			this.appSettings = appSettings;
		}

		public void Load()
		{
			EnsureFileExists(appSettings.ComicsFile);
			var json = File.ReadAllText(appSettings.ComicsFile);
			taskCache = JsonConvert.DeserializeObject<List<ComicTask>>(json) ?? new List<ComicTask>();
		}

		public void Add(ComicTask comic)
		{
			taskCache.Add(comic);
			PersistTasks();
		}

		private void PersistTasks()
		{
			File.WriteAllText(appSettings.ComicsFile, JsonConvert.SerializeObject(taskCache, Formatting.Indented));
		}

		public void Update(ComicTask comic)
		{
			PersistTasks();
		}

		public void Delete(ComicTask comic)
		{
			taskCache.Remove(comic);
			PersistTasks();
		}

		/// <summary>
		/// Returns a list with all the tasks in the database
		/// </summary>
		/// <returns></returns>
		public IList<ComicTask> RetrieveAllTasks()
		{
			return new List<ComicTask>(taskCache);
		}

		public IList<ComicTask> RetrieveActiveTasksByComicInfoFile(string comicInfoFile)
		{
			var tasks = new List<ComicTask>();
			foreach (var task in taskCache)
			{
				if (task.ComicInfoFile == comicInfoFile)
					tasks.Add(task);
			}

			return tasks;
		}

		private static void EnsureFileExists(string file)
		{
			if (File.Exists(file))
				return;

			File.Create(file).Close();
		}

	}
}