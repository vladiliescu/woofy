using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
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
                    case TaskStatus.Paused:
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
                case TaskStatus.Paused:
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
            _tasksController.Initialize();

            dgvwTasks.AutoGenerateColumns = false;
            dgvwTasks.DataSource = _tasksController.Tasks;
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

        private void toolStripButtonAbout_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void toolStripButtonSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }
        #endregion
    }
}