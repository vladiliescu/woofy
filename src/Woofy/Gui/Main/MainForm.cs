using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Woofy.Core;
using Woofy.Properties;
using Woofy.Settings;
using Woofy.Updates;

namespace Woofy.Gui.Main
{
    public partial class MainForm : BaseForm
    {
		public IMainController Controller { get; set; }

    	public MainForm()
        {
            InitializeComponent();
        }

    	private void MainForm_Load(object sender, EventArgs e)
        {
            InitControls();
			StartAllComics();

            if (UserSettings.AutomaticallyCheckForUpdates)
                //UpdateController.CheckForUpdatesAsync(this, false);
                UpdateManager.CheckForUpdatesAsync(false, this);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (!UserSettings.MinimizeToTray)
                return;

            if (WindowState == FormWindowState.Minimized)
                Hide();
        }


        #region Events - dgvwTasks
        private void dgvwTasks_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvwTasks.Rows)
            {
                var task = (Comic)row.DataBoundItem;

                row.Cells["ComicsColumn"].Value = string.Format("{0}", task.DownloadedComics);

                switch (task.Status)
                {
                    case TaskStatus.Stopped:
                        row.Cells["TaskStatusColumn"].Value = Resources.Paused;
                        break;
                    case TaskStatus.Running:
                        row.Cells["TaskStatusColumn"].Value = Resources.Running;
                        break;
                    case TaskStatus.Finished:
                        DisplayDownloadOutcome(row, task.DownloadOutcome, task.CurrentUrl);
                        break;
                    default:
                        break;
                }

            }
        }

        private void DisplayDownloadOutcome(DataGridViewRow row, DownloadOutcome downloadOutcome, string url)
        {
            Bitmap icon;
            string toolTip;

            switch (downloadOutcome)
            {
                case DownloadOutcome.Successful:
                    icon = Resources.Finished;
                    toolTip = "";
                    break;
                case DownloadOutcome.NoStripMatchesRuleBroken:
                    icon = Resources.Warning;
                    toolTip = string.Format("No strips have been found on {0}. In order to allow missing strips, use the allowMissingStrips attribute in the comic definition.", url);
                    break;
                case DownloadOutcome.MultipleStripMatchesRuleBroken:
                    icon = Resources.Warning;
                    toolTip = string.Format("Multiple strips have been found on {0}. In order to allow multiple strips, use the allowMultipleStrips attribute in the comic definition.", url);
                    break;
                case DownloadOutcome.Error:
                    icon = Resources.Error;
                    toolTip = string.IsNullOrEmpty(url) ? 
                        "An error has occurred while downloading the latest strip." :
                        string.Format("An error has occurred while downloading the strip at {0}.", url);
                    break;
                default:
                    throw new System.ComponentModel.InvalidEnumArgumentException("downloadOutcome", (int)downloadOutcome, typeof(DownloadOutcome));
            }

            row.Cells["TaskStatusColumn"].Value = icon;
            foreach (DataGridViewCell cell in row.Cells)
            {
                cell.ToolTipText = toolTip;
            }
        }

        private void dgvwTasks_DoubleClick(object sender, EventArgs e)
        {
            if (dgvwTasks.SelectedRows.Count == 0)
                return;

            var task = (Comic)dgvwTasks.SelectedRows[0].DataBoundItem;
            if (task.Status == TaskStatus.Finished)
                Controller.OpenFolder(task);
            else
                ToggleSelectedTasksState();
        }

        private void dgvwTasks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvwTasks.SelectedRows.Count == 0)
            {
                toolStripButtonPauseTask.Enabled = false;
                toolStripButtonOpenFolder.Enabled = false;
                return;
            }
            toolStripButtonPauseTask.Enabled = true;
            toolStripButtonOpenFolder.Enabled = true;

            var task = (Comic)dgvwTasks.SelectedRows[0].DataBoundItem;

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

        #region Helper Methods
        private void ToggleSelectedTasksState()
        {
            if (dgvwTasks.SelectedRows.Count == 0)
                return;
			
			var selectedRows = (from row in dgvwTasks.SelectedRows.Cast<DataGridViewRow>() select (Comic)row.DataBoundItem).ToArray();
			Controller.ToggleBotState(selectedRows);
        }

        private void OpenSelectedTaskFolder()
        {
            if (dgvwTasks.SelectedRows.Count == 0)
                return;

            var task = (Comic)dgvwTasks.SelectedRows[0].DataBoundItem;
            Controller.OpenFolder(task);
        }

        /// <summary>
        /// Starts all the existing tasks.
        /// </summary>
        private void StartAllComics()
        {
			Controller.StartBots((from row in dgvwTasks.Rows.Cast<DataGridViewRow>() select (Comic)row.DataBoundItem).ToArray());
        }

        /// <summary>
        /// Stops all the existing tasks.
        /// </summary>
        private void StopAllTasks()
        {
			Controller.StopBots((from row in dgvwTasks.Rows.Cast<DataGridViewRow>() select (Comic)row.DataBoundItem).ToArray());
        }
        #endregion

        #region Events - Tool Strip Menus

       private void toolStripMenuItemPauseTask_Click(object sender, EventArgs e)
        {
            ToggleSelectedTasksState();
        }

        private void toolStripMenuItemOpenTaskFolder_Click(object sender, EventArgs e)
        {
            OpenSelectedTaskFolder();
        }
        #endregion

        #region Events - Tool Strip Buttons
        private void OnSelectComics(object sender, EventArgs e)
        {
			Controller.DisplayComicSelectionForm();
        }

        private void toolStripButtonPauseTask_Click(object sender, EventArgs e)
        {
            ToggleSelectedTasksState();
        }

        private void toolStripButtonOpenFolder_Click(object sender, EventArgs e)
        {
            OpenSelectedTaskFolder();
        }

        private void About_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void toolStripButtonSettings_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        private void CheckForUpdates_Click(object sender, EventArgs e)
        {
            UpdateManager.CheckForUpdatesAsync(true, this);
        }

        private void DebugDefinitions_Click(object sender, EventArgs e)
        {
            StopAllTasks();

            var definitionsDebugForm = new DefinitionsDebugForm();            
            definitionsDebugForm.ShowDialog();
        }

		private void OnHomePageClick(object sender, EventArgs e)
		{
			Process.Start(AppSettingsOld.AuthorHomePage);
		}
        #endregion        

        #region Events - Tray Tool Strip Menus
        private void hideShowWoofyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Visible)
            {
                WindowState = FormWindowState.Minimized;
                Visible = false;
            }
            else
            {
                Visible = true;
                WindowState = FormWindowState.Normal;
            }
        }

        private void stopAllTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopAllTasks();
        }

        private void startAllTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartAllComics();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        } 
        #endregion

        #region Events - notifyIcon
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Visible = true;
            WindowState = FormWindowState.Normal;
        } 
        #endregion

        #region Public Threading Methods
        /// <summary>
        /// Initializes the download progress form for downloading updates, on the UI thread. Also, hides itself.
        /// </summary>
        /// <param name="downloadFileSize">The size of the file to download. Needed for initializing the download progress form.</param>
        public void InitializeUpdatesDownloadProgressForm(int downloadFileSize)
        {
            Invoke(new MethodInvoker(
                delegate
                {
                    var downloadProgressForm = new DownloadProgressForm(downloadFileSize);
                    downloadProgressForm.Show();

                    Hide();
                }
            ));            
        }

        /// <summary>
        /// Reports an error message to the user.
        /// </summary>
        /// <param name="errorMessage">The error message to report to the user.</param>
        public void ReportError(string errorMessage)
        {
            Invoke(new MethodInvoker(
                       () =>
                       MessageBox.Show(errorMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                       ));
        }

        public DialogResult DisplayMessageBox(string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            var result = DialogResult.None;
            
            Invoke(new MethodInvoker(
                delegate
                {
                    result = MessageBox.Show(message, Application.ProductName, buttons, icon);
                }
            ));

            return result;
        }
        #endregion        

		void InitControls()
		{
			var appIcon = new Icon(typeof(Program), "Woofy.ico");
			Icon = 
				notifyIcon.Icon = appIcon;


			dgvwTasks.AutoGenerateColumns = false;
			dgvwTasks.DataSource = Controller.Tasks;

			var splitButton = new ToolStripSplitButton("About..", Resources.About);
			splitButton.ButtonClick += About_Click;
			splitButton.DropDown.Items.Add("Debug comic definitions..", Resources.DebugDefinitions, DebugDefinitions_Click);
			splitButton.DropDown.Items.Add(new ToolStripSeparator());
			splitButton.DropDown.Items.Add("Check for updates", Resources.CheckForUpdates, CheckForUpdates_Click);
			splitButton.DropDown.Items.Add("About..", Resources.About, About_Click);
			splitButton.DropDown.Items.Add(new ToolStripSeparator());
			splitButton.DropDown.Items.Add("vladiliescu.ro", Resources.vladiliescu_ro, OnHomePageClick);
			splitButton.Alignment = ToolStripItemAlignment.Right;

			toolStrip.Items.Insert(0, splitButton);
		}
    }
}