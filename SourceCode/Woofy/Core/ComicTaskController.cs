using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

using Woofy.Enums;
using Woofy.Properties;

namespace Woofy.Core
{
    public class ComicTasksController
    {
        #region Instance Members
        private List<ComicsProvider> _comicProviders = new List<ComicsProvider>();
        private DataGridView _tasksGrid;
        private ComicTasksComparer _tasksComparer = new ComicTasksComparer();
        #endregion

        #region Public Properties
        private BindingList<ComicTask> _tasks;
        public BindingList<ComicTask> Tasks
        {
            get { return _tasks; }
        }
        #endregion

        #region .ctor
        public ComicTasksController(DataGridView tasksGrid)
        {
            _tasksGrid = tasksGrid;
        }
        #endregion

        #region Public Methods
        public void Initialize()
        {
            _tasks = new BindingList<ComicTask>(ComicTask.RetrieveAllTasks());

            foreach (ComicTask task in _tasks)
            {
                AddComicsProviderAndStartDownload(task);
            }
        }

        /// <summary>
        /// Adds a new task to the tasks list and database. Also starts its download.
        /// </summary>
        /// <returns>True if the task has been added successfully, false otherwise.</returns>
        public bool AddNewTask(ComicTask task)
        {
            if (ComicTask.RetrieveActiveTasksByComicInfoFile(task.ComicInfoFile).Count > 0)
                return false;

            task.Create();
            _tasks.Add(task);
            AddComicsProviderAndStartDownload(task);

            return true;
        }

        /// <summary>
        /// Stops the specified task's download and deletes it from the database.
        /// </summary>
        /// <param name="task"></param>
        public void DeleteTask(ComicTask task)
        {
            int index = _tasks.IndexOf(task);
            ComicsProvider comicsProvider = _comicProviders[index];
            if (task.Status == TaskStatus.Running)
                comicsProvider.StopDownload();

            task.Delete();

            _tasks.RemoveAt(index);
            _comicProviders.RemoveAt(index);
        }

        /// <summary>
        /// Pauses/unpauses a task, depending on its current state.
        /// </summary>
        public void ToggleTaskState(ComicTask task, bool resetTasksBindings)
        {
            int index = _tasks.IndexOf(task);
            ComicsProvider comicsProvider = _comicProviders[index];

            switch (task.Status)
            {
                case TaskStatus.Paused:
                    task.Status = TaskStatus.Running;
                    int comicsToDownload = task.ComicsToDownload.HasValue ? (int)(task.ComicsToDownload.Value - task.DownloadedComics) : ComicsProvider.AllAvailableComics;
                    comicsProvider.DownloadComicsAsync(comicsToDownload, task.CurrentUrl);
                    break;
                case TaskStatus.Running:
                    task.Status = TaskStatus.Paused;
                    comicsProvider.StopDownload();
                    break;
            }

            task.Update();

            if (resetTasksBindings)
                ResetTasksBindings();
        }

        /// <summary>
        /// Opens the folder associated with the specified task, using Windows Explorer.
        /// </summary>
        public void OpenTaskFolder(ComicTask task)
        {
            if (Directory.Exists(task.DownloadFolder))
                Process.Start(task.DownloadFolder);
        }

        public void ResetTasksBindings()
        {
            _tasks.ResetBindings();
        }
        #endregion

        #region Helper Methods
        private void AddComicsProviderAndStartDownload(ComicTask task)
        {
            ComicInfo comicInfo = new ComicInfo(task.ComicInfoFile);

            ComicsProvider comicsProvider = new ComicsProvider(comicInfo, task.DownloadFolder);
            _comicProviders.Add(comicsProvider);

            comicsProvider.DownloadComicCompleted += new EventHandler<DownloadSingleComicCompletedEventArgs>(DownloadComicCompletedCallback);
            comicsProvider.DownloadCompleted += new EventHandler(DownloadComicsCompletedCallback);

            if (task.Status == TaskStatus.Running)
            {
                int comicsToDownload = task.ComicsToDownload.HasValue ? (int)(task.ComicsToDownload.Value - task.DownloadedComics) : ComicsProvider.AllAvailableComics;
                if (string.IsNullOrEmpty(task.CurrentUrl))
                    comicsProvider.DownloadComicsAsync(comicsToDownload);
                else
                    comicsProvider.DownloadComicsAsync(comicsToDownload, task.CurrentUrl);
            }
        }

        private void DownloadComicCompletedCallback(object sender, DownloadSingleComicCompletedEventArgs e)
        {
            _tasksGrid.Invoke(new MethodInvoker(
                delegate()
                {
                    ComicsProvider provider = (ComicsProvider)sender;

                    int index = _comicProviders.IndexOf(provider);
                    ComicTask task = (ComicTask)_tasks[index];
                    task.DownloadedComics++;
                    task.CurrentUrl = e.CurrentUrl;
                    task.Update();

                    ResetTasksBindings();
                }
            ));
        }

        private void DownloadComicsCompletedCallback(object sender, EventArgs e)
        {
            _tasksGrid.Invoke(new MethodInvoker(
                delegate()
                {
                    ComicsProvider comicsProvider = (ComicsProvider)sender;

                    int index = _comicProviders.IndexOf(comicsProvider);
                    ComicTask task = (ComicTask)_tasks[index];
                    task.Status = TaskStatus.Finished;
                    task.Delete();

                    ResetTasksBindings();
                }
            ));
        }
        #endregion
    }
}