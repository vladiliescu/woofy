using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using Woofy.Core;
using Woofy.Enums;
using Woofy.Properties;

namespace Woofy.Gui
{
    public partial class MainForm : Form
    {
        #region .ctor
        public MainForm()
        {
            InitializeComponent();

            _tasksController = new ComicTasksController(dgvwTasks);
        }
        #endregion

        #region Instance Members
        private ComicTasksController _tasksController;
        #endregion

        #region Events - Form
        private void MainForm_Load(object sender, EventArgs e)
        {
            InitControls();
            UpdateController.CheckForUpdatesAsync(this, false);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (!Settings.Default.MinimizeToTray)
                return;

            if (this.WindowState == FormWindowState.Minimized)
                this.Hide();
        }
        #endregion

        #region Events - dgvwTasks
        private void dgvwTasks_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvwTasks.Rows)
            {
                ComicTask task = (ComicTask)row.DataBoundItem;

                string comicsToDownload = task.ComicsToDownload.HasValue ? task.ComicsToDownload.Value.ToString() : "-";

                row.Cells["ComicsColumn"].Value = string.Format("{0}/{1}", task.DownloadedComics, comicsToDownload);

                switch (task.Status)
                {
                    case TaskStatus.Stopped:
                        row.Cells["TaskStatusColumn"].Value = Properties.Resources.Paused;
                        break;
                    case TaskStatus.Running:
                        row.Cells["TaskStatusColumn"].Value = Properties.Resources.Running;
                        break;
                    case TaskStatus.Finished:
                        row.Cells["TaskStatusColumn"].Value = Properties.Resources.Finished;
                        break;
                    default:
                        break;
                }

            }
        }

        private void dgvwTasks_DoubleClick(object sender, EventArgs e)
        {
            if (dgvwTasks.SelectedRows.Count == 0)
                return;

            ComicTask task = (ComicTask)dgvwTasks.SelectedRows[0].DataBoundItem;
            if (task.Status == TaskStatus.Finished)
                _tasksController.OpenTaskFolder(task);
            else
                ToggleSelectedTasksState();
        }

        private void dgvwTasks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvwTasks.SelectedRows.Count == 0)
            {
                toolStripButtonPauseTask.Enabled = false;
                toolStripButtonDeleteTask.Enabled = false;
                toolStripButtonOpenFolder.Enabled = false;
                return;
            }
            toolStripButtonPauseTask.Enabled = true;
            toolStripButtonDeleteTask.Enabled = true;
            toolStripButtonOpenFolder.Enabled = true;

            ComicTask task = (ComicTask)dgvwTasks.SelectedRows[0].DataBoundItem;

            switch (task.Status)
            {
                case TaskStatus.Stopped:
                    toolStripButtonPauseTask.Enabled = true;
                    toolStripButtonPauseTask.Image = Resources.Running;
                    toolStripButtonPauseTask.Text = "Unpause";
                    break;
                case TaskStatus.Running:
                    toolStripButtonPauseTask.Enabled = true;
                    toolStripButtonPauseTask.Image = Resources.Paused;
                    toolStripButtonPauseTask.Text = "Pause";
                    break;
                case TaskStatus.Finished:
                    toolStripButtonPauseTask.Enabled = false;                    
                    break;
            }
        }
        #endregion

        #region Initialization Methods
        private void InitControls()
        {
            Icon appIcon = new Icon(typeof(Program), "Woofy.ico");

            this.Icon = 
                notifyIcon.Icon = appIcon;

            _tasksController.Initialize();

            dgvwTasks.AutoGenerateColumns = false;
            dgvwTasks.DataSource = _tasksController.Tasks;

            ToolStripSplitButton splitButton = new ToolStripSplitButton("Check for updates", Resources.CheckForUpdates);
            splitButton.DropDown.Items.Add("Check for updates", Resources.CheckForUpdates, new EventHandler(CheckForUpdates_Click));
            splitButton.DropDown.Items.Add("About...", Resources.About, new EventHandler(About_Click));
            splitButton.Alignment = ToolStripItemAlignment.Right;
            splitButton.ButtonClick += new EventHandler(CheckForUpdates_Click);

            toolStrip.Items.Insert(0, splitButton);
        }
        #endregion

        #region Helper Methods
        private void AddTask()
        {
            TaskDetailsForm taskDetails = new TaskDetailsForm(_tasksController);
            taskDetails.ShowDialog();
        }

        private void DeleteSelectedTasks()
        {
            if (dgvwTasks.SelectedRows.Count == 0)
                return;

            string message = dgvwTasks.SelectedRows.Count > 1 ?
                "Are you sure you want to delete the selected tasks?" :
                "Are you sure you want to delete the selected task?";

            if (MessageBox.Show(message, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            foreach (DataGridViewRow selectedRow in dgvwTasks.SelectedRows)
            {
                ComicTask task = (ComicTask)selectedRow.DataBoundItem;
                _tasksController.DeleteTask(task);
            }
        }

        private void ToggleSelectedTasksState()
        {
            if (dgvwTasks.SelectedRows.Count == 0)
                return;

            foreach (DataGridViewRow selectedRow in dgvwTasks.SelectedRows)
            {
                ComicTask task = (ComicTask)selectedRow.DataBoundItem;
                _tasksController.ToggleTaskState(task, false);
            }

            _tasksController.ResetTasksBindings();
        }

        private void OpenSelectedTaskFolder()
        {
            if (dgvwTasks.SelectedRows.Count == 0)
                return;

            ComicTask task = (ComicTask)dgvwTasks.SelectedRows[0].DataBoundItem;

            _tasksController.OpenTaskFolder(task);
        }

        /// <summary>
        /// Starts all the existing tasks.
        /// </summary>
        private void StartAllTasks()
        {
            foreach (DataGridViewRow row in dgvwTasks.Rows)
            {
                ComicTask task = (ComicTask)row.DataBoundItem;
                _tasksController.StartTask(task);
            }

            _tasksController.ResetTasksBindings();
        }

        /// <summary>
        /// Stops all the existing tasks.
        /// </summary>
        private void StopAllTasks()
        {
            foreach (DataGridViewRow row in dgvwTasks.Rows)
            {
                ComicTask task = (ComicTask)row.DataBoundItem;
                _tasksController.StopTask(task);
            }

            _tasksController.ResetTasksBindings();
        }
        #endregion

        #region Events - Tool Strip Menus

        private void toolStripMenuItemNewTask_Click(object sender, EventArgs e)
        {
            AddTask();
        }

        private void toolStripMenuItemPauseTask_Click(object sender, EventArgs e)
        {
            ToggleSelectedTasksState();
        }

        private void toolStripMenuItemDeleteTask_Click(object sender, EventArgs e)
        {
            DeleteSelectedTasks();
        }

        private void toolStripMenuItemOpenTaskFolder_Click(object sender, EventArgs e)
        {
            OpenSelectedTaskFolder();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }
        #endregion

        #region Events - Tool Strip Buttons
        private void toolStripButtonAddTask_Click(object sender, EventArgs e)
        {
            AddTask();
        }

        private void toolStripButtonPauseTask_Click(object sender, EventArgs e)
        {
            ToggleSelectedTasksState();
        }

        private void toolStripButtonDeleteTask_Click(object sender, EventArgs e)
        {
            DeleteSelectedTasks();
        }

        private void toolStripButtonOpenFolder_Click(object sender, EventArgs e)
        {
            OpenSelectedTaskFolder();
        }

        private void About_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void toolStripButtonSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        private void CheckForUpdates_Click(object sender, EventArgs e)
        {
            UpdateController.CheckForUpdatesAsync(this, true);
        }
        #endregion        

        #region Events - Tray Tool Strip Menus
        private void hideShowWoofyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Visible = false;
            }
            else
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void stopAllTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopAllTasks();
        }

        private void startAllTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartAllTasks();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        } 
        #endregion

        #region Events - notifyIcon
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
        } 
        #endregion

        #region Public Threading Methods
        /// <summary>
        /// Initializes the download progress form for downloading updates, on the UI thread. Also, hides itself.
        /// </summary>
        /// <param name="downloadFileSize">The size of the file to download. Needed for initializing the download progress form.</param>
        public void InitializeUpdatesDownloadProgressForm(int downloadFileSize)
        {
            this.Invoke(new MethodInvoker(
                delegate
                {
                    DownloadProgressForm downloadProgressForm = new DownloadProgressForm(downloadFileSize);
                    downloadProgressForm.Show();

                    this.Hide();
                }
            ));            
        }
        #endregion
    }
}