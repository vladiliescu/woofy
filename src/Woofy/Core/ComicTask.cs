using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Woofy.Settings;

namespace Woofy.Core
{
    public class ComicTask
    {
		private static readonly IList<ComicTask> TaskCache;

		static ComicTask()
		{
			EnsureFileExists(ApplicationSettings.DatabaseConnectionString);
			var json = File.ReadAllText(ApplicationSettings.DatabaseConnectionString);
			TaskCache = JsonConvert.DeserializeObject<List<ComicTask>>(json) ?? new List<ComicTask>();
		}

    	private static void EnsureFileExists(string file)
    	{
			if (File.Exists(file))
				return;

			File.Create(file).Close();
    	}

    	#region Properties

    	public long Id { get; private set; }

    	public DownloadOutcome DownloadOutcome { get; set; }

    	public string Name { get; private set; }

    	public string ComicInfoFile { get; private set; }

    	public long DownloadedComics { get; set; }

    	public long? ComicsToDownload { get; private set; }

    	public string DownloadFolder { get; private set; }

    	public long OrderNumber { get; set; }

    	public TaskStatus Status { get; set; }

    	public string CurrentUrl { get; set; }

    	#endregion

        #region .ctors
        public ComicTask(string name, string comicInfoFile, long? comicsToDownload, string downloadFolder, string currentUrl)
            : this(-1, name, comicInfoFile, 0, comicsToDownload, downloadFolder, 1, currentUrl, TaskStatus.Running)
        {
        }

        private ComicTask(long id, string name, string comicInfoFile, long downloadedComics, long? comicsToDownload, string downloadFolder, long orderNumber, string currentUrl, TaskStatus status)
        {
            Id = id;
            Name = name;
            ComicInfoFile = comicInfoFile;
            DownloadedComics = downloadedComics;
            ComicsToDownload = comicsToDownload;
            DownloadFolder = downloadFolder;
            OrderNumber = orderNumber;
            CurrentUrl = currentUrl;
            Status = status;
        }
        #endregion

        #region Public CRUD Methods
        public void Create()
        {
			TaskCache.Add(this);
			PersistTasks();
        }

    	private void PersistTasks()
    	{
			File.WriteAllText(ApplicationSettings.DatabaseConnectionString, JsonConvert.SerializeObject(TaskCache, Formatting.Indented));
    	}

    	public void Update()
        {
			PersistTasks();
        }

        public void Delete()
        {
			TaskCache.Remove(this);
			PersistTasks();
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Returns a list with all the tasks in the database
        /// </summary>
        /// <returns></returns>
        public static IList<ComicTask> RetrieveAllTasks()
        {
			return new List<ComicTask>(TaskCache);
        }

		public static IList<ComicTask> RetrieveActiveTasksByComicInfoFile(string comicInfoFile)
        {
			var tasks = new List<ComicTask>();
			foreach (var task in TaskCache)
			{
				if (task.ComicInfoFile == comicInfoFile)
					tasks.Add(task);
			}

			return tasks;
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return string.Format("{0}.{1}", Id, Name);
        }
        #endregion
    }
}
