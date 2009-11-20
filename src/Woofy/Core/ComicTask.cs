using System;
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
			//TaskCache = new List<ComicTask>();
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
        private long _id;
        public long Id
        {
            get { return _id; }
        }

        private DownloadOutcome downloadOutcome;
        public DownloadOutcome DownloadOutcome
        {
            get { return downloadOutcome; }
            set { downloadOutcome = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
        }

        private string _comicInfoFile;
        public string ComicInfoFile
        {
            get { return _comicInfoFile; }
        }

        private long _downloadedComics;
        public long DownloadedComics
        {
            get { return _downloadedComics; }
            set { _downloadedComics = value; }
        }

        private long? _comicsToDownload;
        public long? ComicsToDownload
        {
            get { return _comicsToDownload; }
        }

        private string _downloadFolder;
        public string DownloadFolder
        {
            get { return _downloadFolder; }
        }

        private long _orderNumber;
        public long OrderNumber
        {
            get { return _orderNumber; }
            set { _orderNumber = value; }
        }

        private TaskStatus _status;
        public TaskStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private string _currentUrl;
        public string CurrentUrl
        {
            get { return _currentUrl; }
            set { _currentUrl = value; }
        }
        #endregion

        #region .ctors
        public ComicTask(string name, string comicInfoFile, long? comicsToDownload, string downloadFolder, string currentUrl)
            : this(-1, name, comicInfoFile, 0, comicsToDownload, downloadFolder, /*GetLargestOrderNumber() +*/ 1, currentUrl, TaskStatus.Running)
        {
        }

        private ComicTask(long id, string name, string comicInfoFile, long downloadedComics, long? comicsToDownload, string downloadFolder, long orderNumber, string currentUrl, TaskStatus status)
        {
            _id = id;
            _name = name;
            _comicInfoFile = comicInfoFile;
            _downloadedComics = downloadedComics;
            _comicsToDownload = comicsToDownload;
            _downloadFolder = downloadFolder;
            _orderNumber = orderNumber;
            _currentUrl = currentUrl;
            _status = status;
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

        #region Helper Static Methods
		//private static long GetLargestOrderNumber()
		//{
		//    SQLiteConnection connection = new SQLiteConnection(ConnectionString);
		//    SQLiteCommand command = new SQLiteCommand("SELECT max(OrderNumber) FROM [ComicTasks] WHERE Status <> ?", connection);
		//    command.Parameters.Add("Status", DbType.Int64);
		//    command.Parameters["Status"].Value = (long)TaskStatus.Finished;

		//    connection.Open();
		//    try
		//    {
		//        object o = command.ExecuteScalar();
		//        if (o == DBNull.Value)
		//            return 0;
		//        else
		//            return (long)o;
		//    }
		//    finally
		//    {
		//        connection.Close();
		//    }
		//}

        #endregion

        #region Overrides
        public override string ToString()
        {
            return string.Format("{0}.{1}", Id, Name);
        }
        #endregion
    }
}
