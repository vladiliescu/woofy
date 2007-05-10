using System;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;


using Woofy.Enums;
using Woofy.Properties;

namespace Woofy.Core
{
    public class ComicTask
    {
        #region Constants
        private static readonly string ConnectionString;
        #endregion

        #region Properties
        private long _id;
        public long Id
        {
            get { return _id; }
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
            : this(-1, name, comicInfoFile, 0, comicsToDownload, downloadFolder, GetLargestOrderNumber() + 1, currentUrl, TaskStatus.Running)
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

        static ComicTask()
        {
            SQLiteConnectionStringBuilder connectionStringBuilder = new SQLiteConnectionStringBuilder(Settings.Default.ConnectionString);
            connectionStringBuilder.DataSource = AppDomain.CurrentDomain.BaseDirectory + connectionStringBuilder.DataSource;
            ConnectionString = connectionStringBuilder.ConnectionString;
        }
        #endregion

        #region Public CRUD Methods
        public void Create()
        {
            SQLiteConnection connection = new SQLiteConnection(ConnectionString);
            SQLiteCommand command = new SQLiteCommand(
                @"INSERT INTO [ComicTasks] 
                    (Name, ComicInfoFile, DownloadedComics, ComicsToDownload, DownloadFolder, OrderNumber, CurrentUrl, Status) 
                    VALUES 
                    (?, ?, ?, ?, ?, ?, ?, ?); 
                SELECT last_insert_rowid();", connection);
            command.Parameters.Add("Name", DbType.String, 128);
            command.Parameters.Add("ComicInfoFile", DbType.String, 256);
            command.Parameters.Add("DownloadedComics", DbType.Int64);
            command.Parameters.Add("ComicsToDownload", DbType.Int64);
            command.Parameters.Add("DownloadFolder", DbType.String, 512);
            command.Parameters.Add("OrderNumber", DbType.Int64);
            command.Parameters.Add("CurrentUrl", DbType.String, 512);
            command.Parameters.Add("Status", DbType.Int64);

            command.Parameters["Name"].Value = _name;
            command.Parameters["ComicInfoFile"].Value = _comicInfoFile;
            command.Parameters["DownloadedComics"].Value = _downloadedComics;
            if (_comicsToDownload.HasValue)
                command.Parameters["ComicsToDownload"].Value = _comicsToDownload;
            else
                command.Parameters["ComicsToDownload"].Value = DBNull.Value;
            command.Parameters["DownloadFolder"].Value = _downloadFolder;
            command.Parameters["OrderNumber"].Value = _orderNumber;
            if (_currentUrl != null)
                command.Parameters["CurrentUrl"].Value = _currentUrl;
            else
                command.Parameters["CurrentUrl"].Value = DBNull.Value;
            command.Parameters["Status"].Value = (long)_status;

            connection.Open();
            long taskId;
            try
            {
                taskId = (long)command.ExecuteScalar();
            }
            finally
            {
                connection.Close();
            }

            _id = taskId;
        }

        public void Update()
        {
            SQLiteConnection connection = new SQLiteConnection(ConnectionString);
            SQLiteCommand command = new SQLiteCommand(
                @"UPDATE [ComicTasks]
                        SET Name = ?, 
                        ComicInfoFile = ?, 
                        DownloadedComics = ?, 
                        ComicsToDownload = ?, 
                        DownloadFolder = ?,
                        OrderNumber = ?, 
                        CurrentUrl = ?, 
                        Status = ? 
                WHERE Id = ?", connection);
            command.Parameters.Add("Name", DbType.String, 128);
            command.Parameters.Add("ComicInfoFile", DbType.String, 256);
            command.Parameters.Add("DownloadedComics", DbType.Int64);
            command.Parameters.Add("ComicsToDownload", DbType.Int64);
            command.Parameters.Add("DownloadFolder", DbType.String, 512);
            command.Parameters.Add("OrderNumber", DbType.Int64);
            command.Parameters.Add("CurrentUrl", DbType.String, 512);
            command.Parameters.Add("Status", DbType.Int64);
            command.Parameters.Add("Id", DbType.Int64);

            command.Parameters["Name"].Value = _name;
            command.Parameters["ComicInfoFile"].Value = _comicInfoFile;
            command.Parameters["DownloadedComics"].Value = _downloadedComics;
            if (_comicsToDownload.HasValue)
                command.Parameters["ComicsToDownload"].Value = _comicsToDownload;
            else
                command.Parameters["ComicsToDownload"].Value = DBNull.Value;
            command.Parameters["DownloadFolder"].Value = _downloadFolder;
            command.Parameters["OrderNumber"].Value = _orderNumber;
            if (_currentUrl != null)
                command.Parameters["CurrentUrl"].Value = _currentUrl;
            else
                command.Parameters["CurrentUrl"].Value = DBNull.Value;
            command.Parameters["Status"].Value = (long)_status;
            command.Parameters["Id"].Value = _id;

            connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public void Delete()
        {
            SQLiteConnection connection = new SQLiteConnection(ConnectionString);
            SQLiteCommand command = new SQLiteCommand("DELETE FROM [ComicTasks] WHERE Id = ?", connection);
            command.Parameters.Add("Id", DbType.Int64);

            command.Parameters["Id"].Value = _id;

            connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Returns a list with all the tasks in the database
        /// </summary>
        /// <returns></returns>
        public static ComicTasksList RetrieveAllTasks()
        {
            SQLiteConnection connection = new SQLiteConnection(ConnectionString);
            SQLiteCommand command = new SQLiteCommand("SELECT Id, Name, ComicInfoFile, DownloadedComics, ComicsToDownload, DownloadFolder, OrderNumber, CurrentUrl, Status FROM [ComicTasks]", connection);

            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
            {
                return RetrieveTasksWithReader(reader);
            }
        }

        public static ComicTasksList RetrieveActiveTasksByComicInfoFile(string comicInfoFile)
        {
            SQLiteConnection connection = new SQLiteConnection(ConnectionString);
            SQLiteCommand command = new SQLiteCommand("SELECT Id, Name, ComicInfoFile, DownloadedComics, ComicsToDownload, DownloadFolder, OrderNumber, CurrentUrl, Status FROM [ComicTasks] WHERE ComicInfoFile = ? AND Status <> ?", connection);
            command.Parameters.Add("ComicInfoFile", DbType.String, 256);
            command.Parameters.Add("Status", DbType.Int64);

            command.Parameters["ComicInfoFile"].Value = comicInfoFile;
            command.Parameters["Status"].Value = TaskStatus.Finished;

            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
            {
                return RetrieveTasksWithReader(reader);
            }
        }
        #endregion

        #region Helper Static Methods
        private static long GetLargestOrderNumber()
        {
            SQLiteConnection connection = new SQLiteConnection(ConnectionString);
            SQLiteCommand command = new SQLiteCommand("SELECT max(OrderNumber) FROM [ComicTasks] WHERE Status <> ?", connection);
            command.Parameters.Add("Status", DbType.Int64);
            command.Parameters["Status"].Value = (long)TaskStatus.Finished;

            connection.Open();
            try
            {
                object o = command.ExecuteScalar();
                if (o == DBNull.Value)
                    return 0;
                else
                    return (long)o;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Retrieves a list of tasks using the specified <see cref="SQLiteDataReader"/>.
        /// </summary>
        /// <param name="reader"></param>
        private static ComicTasksList RetrieveTasksWithReader(SQLiteDataReader reader)
        {
            ComicTasksList tasks = new ComicTasksList();

            while (reader.Read())
            {

                long id = reader.GetInt64(0);
                string name = reader.GetString(1);
                string comicInfoFile = reader.GetString(2);
                long downloadedComics = reader.GetInt64(3);
                object comicsToDownloadObject = reader.GetValue(4);
                long? comicsToDownload;
                if (comicsToDownloadObject == DBNull.Value)
                    comicsToDownload = null;
                else
                    comicsToDownload = reader.GetInt64(4);
                string downloadFolder = reader.GetString(5);
                long orderNumber = reader.GetInt64(6);
                object currentUrlObject = reader.GetValue(7);
                string currentUrl;
                if (currentUrlObject == DBNull.Value)
                    currentUrl = null;
                else
                    currentUrl = (string)currentUrlObject;
                long status = reader.GetInt64(8);

                tasks.Add(new ComicTask(id, name, comicInfoFile, downloadedComics, comicsToDownload, downloadFolder, orderNumber, currentUrl, (TaskStatus)status));
            }

            return tasks;
        }

        #endregion

        #region Overrides
        public override string ToString()
        {
            return string.Format("{0}.{1}", Id.ToString(), Name);
        }
        #endregion
    }
}
