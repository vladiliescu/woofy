using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Woofy.Core;
using Woofy.Core.Engine;
using Woofy.Enums;
using Woofy.Flows.AutoUpdate;
using Woofy.Gui;
using Woofy.Properties;
using Woofy.Settings;

namespace Woofy.Flows.Main
{
	public partial class MainForm : Form
	{
		public IMainPresenter Presenter { get; set; }

		public MainForm()
		{
			InitializeComponent();

			RegisterCommands();
		}

		private void RegisterCommands()
		{
			//toolStripMenuItemPauseTask.Click += (o, e) => ToggleSelectedTasksState();
			toolStripMenuItemOpenTaskFolder.Click += (o, e) => OpenSelectedTaskFolder();
			toolStripButtonOpenFolder.Click += (o, e) => OpenSelectedTaskFolder();
			tsbAddComic.Click += (o, e) => Presenter.AddComicRequested();
			//toolStripButtonPauseTask.Click += (o, e) => Presenter.AddComicRequested();
			toolStripButtonSettings.Click += (o, e) =>
			{
				using (var settingsForm = new SettingsForm())
					settingsForm.ShowDialog();
			};
			exitToolStripMenuItem.Click += (o, e) => Application.Exit();
			notifyIcon.MouseDoubleClick += (o, e) => {
        		Visible = true;
        		WindowState = FormWindowState.Normal;
        	};

            txtAppLog.LinkClicked += (o, e) => Presenter.Open(e.LinkText);
            txtAppLog.TextChanged += (o, e) => {
                txtAppLog.SelectionStart = txtAppLog.Text.Length;
                txtAppLog.ScrollToCaret();
            };
		}

		private void OnLoad(object sender, EventArgs e)
		{
			InitControls();

			Presenter.Initialize(this);
            dgvwTasks.DataSource = Presenter.Comics;
		}

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
                Presenter.AddComicRequested();
        }

        #region OBSOLETE
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (!UserSettingsOld.MinimizeToTray)
                return;

            if (WindowState == FormWindowState.Minimized)
                Hide();
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

        private void OnAboutClick(object sender, EventArgs e)
        {
            using (var aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog();
            }
        }

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

        private void OpenSelectedTaskFolder()
        {
            //if (dgvwTasks.SelectedRows.Count == 0)
            //    return;

            //var task = (Comic)dgvwTasks.SelectedRows[0].DataBoundItem;
            //Presenter.OpenFolder(task);
        }

        private void InitControls()
        {
            Icon = notifyIcon.Icon = Resources.PrimaryIcon;

            dgvwTasks.AutoGenerateColumns = false;

            var splitButton = new ToolStripSplitButton("About..", Resources.About);
            splitButton.ButtonClick += OnAboutClick;

            splitButton.DropDown.Items.Add(new ToolStripSeparator());
            splitButton.DropDown.Items.Add("Check for updates", Resources.CheckForUpdates, (o, e) => UpdateManager.CheckForUpdatesAsync(true, this));
            splitButton.DropDown.Items.Add("About..", Resources.About, OnAboutClick);
            splitButton.DropDown.Items.Add(new ToolStripSeparator());
            splitButton.DropDown.Items.Add("vladiliescu.ro", Resources.vladiliescu_ro, (o, e) => Process.Start(AppSettingsOld.AuthorHomePage));
            splitButton.Alignment = ToolStripItemAlignment.Right;

            toolStrip.Items.Insert(0, splitButton);

            txtAppLog.DataBindings.Add(new Binding("Text", Presenter, "AppLog"));
        }

        #endregion

        private void OnGridCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
#warning I should extract these indexes and place them in some instance members
            if (e.ColumnIndex != dgvwTasks.Columns.IndexOf(colStatus) || e.Value == null)
                return;

            var status = (WorkerStatus)e.Value;
            switch (status)
            {
                case WorkerStatus.Paused:
                    e.Value = Resources.Paused;
                    break;
                case WorkerStatus.Running:
                    e.Value = Resources.Running;
                    break;
                case WorkerStatus.Finished:
                    e.Value = Resources.Finished;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnGridCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != dgvwTasks.Columns.IndexOf(colCurrentPage))
                return;

            var value = dgvwTasks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as string;
            if (value == null)
                return;

            Presenter.Open(value);
        }

        private void OnToggleComicState(object sender, EventArgs e)
        {
            if (dgvwTasks.SelectedRows.Count == 0)
                return;
            var comic = (ComicViewModel)dgvwTasks.SelectedRows[0].DataBoundItem;

            Presenter.ToggleComicState(new ComicInputModel { Id = comic.Id });
        }
	}
}