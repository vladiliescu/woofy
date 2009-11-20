using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Woofy.Settings;

namespace Woofy.Core
{
    public class ComicTasksController
    {
        #region Instance Members
        private readonly List<ComicsProvider> _comicProviders = new List<ComicsProvider>();
        private readonly DataGridView _tasksGrid;

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
            string downloadFolder = (Path.IsPathRooted(task.DownloadFolder) ? task.DownloadFolder : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, task.DownloadFolder));
            if (Directory.Exists(downloadFolder))
                Process.Start(downloadFolder);
        }

        public void ResetTasksBindings()
        {
            _tasks.ResetBindings();
        }
        #endregion

        #region Helper Methods
        private void AddComicsProviderAndStartDownload(ComicTask task)
        {
            var comicInfo = new ComicDefinition(task.ComicInfoFile);

            var comicsProvider = new ComicsProvider(comicInfo, task.DownloadFolder);
            _comicProviders.Add(comicsProvider);

            comicsProvider.DownloadComicCompleted += DownloadComicCompletedCallback;
            comicsProvider.DownloadCompleted += DownloadComicsCompletedCallback;

            if (task.Status == TaskStatus.Finished)
            {
                task.Status = TaskStatus.Running;
                task.Update();
            }

            if (task.Status != TaskStatus.Running) 
                return;

            var comicsToDownload = task.ComicsToDownload.HasValue ? (int)(task.ComicsToDownload.Value - task.DownloadedComics) : ComicsProvider.AllAvailableComics;
            if (string.IsNullOrEmpty(task.CurrentUrl))
                comicsProvider.DownloadComicsAsync(comicsToDownload);
            else
                comicsProvider.DownloadComicsAsync(comicsToDownload, task.CurrentUrl);
        }        
        #endregion

        #region Callback Methods
        private void DownloadComicCompletedCallback(object sender, DownloadStripCompletedEventArgs e)
        {
            _tasksGrid.Invoke(new MethodInvoker(
                delegate
                    {
                    var provider = (ComicsProvider)sender;

                    int index = _comicProviders.IndexOf(provider);
                    if (index == -1)    //in case the task has already been deleted.
                        return;

                    ComicTask task = _tasks[index];
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
                    var comicsProvider = (ComicsProvider)sender;

                    var index = _comicProviders.IndexOf(comicsProvider);
                    if (index == -1)    //in case the task has already been deleted.
                        return;

                    var task = _tasks[index];
                    task.Status = e.DownloadOutcome == DownloadOutcome.Cancelled ? TaskStatus.Stopped : TaskStatus.Finished;

                    //only set the currentUrl to null if the outcome is successful
                    if (e.DownloadOutcome == DownloadOutcome.Successful)
                        task.CurrentUrl = null;

                    task.DownloadOutcome = e.DownloadOutcome;
                    task.Update();

                    ResetTasksBindings();

                    if (!UsrSettings.CloseWhenAllComicsHaveFinished)
                        return;

                    var allTasksHaveFinished = true;
                    foreach (var _task in _tasks)
                    {
                        if (_task.Status != TaskStatus.Running) 
                            continue;

                        allTasksHaveFinished = false;
                        break;
                    }

                    if (allTasksHaveFinished)
                        Application.Exit();
                }
            ));
        }
        #endregion
    }
}