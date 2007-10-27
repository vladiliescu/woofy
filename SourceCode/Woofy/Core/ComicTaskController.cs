using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

using Woofy.Properties;
using Woofy.Settings;

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
            switch (task.Status)
            {
                case TaskStatus.Stopped:
                    StartTask(task);
                    break;
                case TaskStatus.Running:
                    StopTask(task);
                    break;
            }

            if (resetTasksBindings)
                ResetTasksBindings();
        }

        /// <summary>
        /// Stops the specified comic task.
        /// </summary>
        /// <param name="task">Comic task to stop.</param>
        public void StopTask(ComicTask task)
        {
            if (task.Status != TaskStatus.Running)
                return;

            int index = _tasks.IndexOf(task);
            ComicsProvider comicsProvider = _comicProviders[index];

            task.Status = TaskStatus.Stopped;
            comicsProvider.StopDownload();

            task.Update();
        }

        /// <summary>
        /// Start the specified comic task.
        /// </summary>
        /// <param name="task">Comic task to start.</param>
        public void StartTask(ComicTask task)
        {
            if (task.Status != TaskStatus.Stopped)
                return;

            int index = _tasks.IndexOf(task);
            ComicsProvider comicsProvider = _comicProviders[index];

            task.Status = TaskStatus.Running;
            int comicsToDownload = task.ComicsToDownload.HasValue ? (int)(task.ComicsToDownload.Value - task.DownloadedComics) : ComicsProvider.AllAvailableComics;
            comicsProvider.DownloadComicsAsync(comicsToDownload, task.CurrentUrl);

            task.Update();
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
            ComicDefinition comicInfo = new ComicDefinition(task.ComicInfoFile);

            ComicsProvider comicsProvider = new ComicsProvider(comicInfo, task.DownloadFolder);
            _comicProviders.Add(comicsProvider);

            comicsProvider.DownloadComicCompleted += new EventHandler<DownloadStripCompletedEventArgs>(DownloadComicCompletedCallback);
            comicsProvider.DownloadCompleted += new EventHandler<DownloadCompletedEventArgs>(DownloadComicsCompletedCallback);

            if (task.Status == TaskStatus.Running)
            {
                int comicsToDownload = task.ComicsToDownload.HasValue ? (int)(task.ComicsToDownload.Value - task.DownloadedComics) : ComicsProvider.AllAvailableComics;
                if (string.IsNullOrEmpty(task.CurrentUrl))//TODO: intra vreodata pe ramura asta?
                    comicsProvider.DownloadComicsAsync(comicsToDownload);
                else
                    comicsProvider.DownloadComicsAsync(comicsToDownload, task.CurrentUrl);
            }
        }        
        #endregion

        #region Callback Methods
        private void DownloadComicCompletedCallback(object sender, DownloadStripCompletedEventArgs e)
        {
            _tasksGrid.Invoke(new MethodInvoker(
                delegate()
                {
                    ComicsProvider provider = (ComicsProvider)sender;

                    int index = _comicProviders.IndexOf(provider);
                    if (index == -1)    //in case the task has already been deleted.
                        return;

                    ComicTask task = (ComicTask)_tasks[index];
                    task.DownloadedComics++;
                    task.CurrentUrl = e.CurrentUrl;
                    task.Update();

                    ResetTasksBindings();
                }
            ));
        }

        private void DownloadComicsCompletedCallback(object sender, DownloadCompletedEventArgs e)
        {
            _tasksGrid.Invoke(new MethodInvoker(
                delegate
                {
                    ComicsProvider comicsProvider = (ComicsProvider)sender;

                    int index = _comicProviders.IndexOf(comicsProvider);
                    if (index == -1)    //in case the task has already been deleted.
                        return;

                    ComicTask task = _tasks[index];
                    if (e.DownloadOutcome == DownloadOutcome.Cancelled)
                        task.Status = TaskStatus.Stopped;
                    else
                        task.Status = TaskStatus.Finished;
                    task.DownloadOutcome = e.DownloadOutcome;
                    task.Delete();

                    ResetTasksBindings();

                    if (!UserSettings.CloseWhenAllComicsHaveFinished)
                        return;

                    bool allTasksHaveFinished = true;
                    foreach (ComicTask _task in _tasks)
                    {
                        if (_task.Status == TaskStatus.Running)
                        {
                            allTasksHaveFinished = false;
                            break;
                        }
                    }

                    if (allTasksHaveFinished)
                        Application.Exit();
                }
            ));
        }
        #endregion
    }
}