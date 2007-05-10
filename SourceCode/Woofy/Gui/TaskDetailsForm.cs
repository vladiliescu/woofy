using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Woofy.Core;
using Woofy.Properties;

namespace Woofy.Gui
{
    public partial class TaskDetailsForm : Form
    {
        #region Instance Members
        private ComicTasksController _tasksController;
        #endregion

        #region .ctor
        public TaskDetailsForm(ComicTasksController tasksController)
        {
            InitializeComponent();

            _tasksController = tasksController;
        }
        #endregion

        #region Events - Form
        private void TaskDetails_Load(object sender, EventArgs e)
        {
            cbComics.DataSource = ComicInfo.GetAvailableComicInfos();

            if (!string.IsNullOrEmpty(Settings.Default.LastUsedComicInfoFile))
            {
                int i = 0;
                foreach (ComicInfo comicInfo in cbComics.Items)
                {
                    if (comicInfo.ComicInfoFile.Equals(Settings.Default.LastUsedComicInfoFile, StringComparison.OrdinalIgnoreCase))
                    {
                        cbComics.SelectedIndex = i;
                        break;
                    }

                    i++;
                }
            }

            if (Settings.Default.LastNumberOfComicsToDownload > 0)
            {
                rbDownloadLast.Checked = true;
                numComicsToDownload.Value = Settings.Default.LastNumberOfComicsToDownload;
            }
            else
            {
                rbDownloadOnlyNew.Checked = true;
            }

            UpdateDownloadFolder();            
        }        
        #endregion

        #region Events - Clicks
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cbComics.SelectedItem == null)
                return;

            ComicInfo comicInfo = (ComicInfo)cbComics.SelectedItem;
            long? comicsToDownload;
            if (rbDownloadLast.Checked)
                comicsToDownload = (long)numComicsToDownload.Value;
            else
                comicsToDownload = null;
            string downloadFolder = txtDownloadFolder.Text;
            string startUrl = chkOverrideStartUrl.Checked ? txtOverrideStartUrl.Text : comicInfo.StartUrl;

            ComicTask task = new ComicTask(comicInfo.FriendlyName, comicInfo.ComicInfoFile, comicsToDownload, downloadFolder, startUrl);
            bool taskAdded = _tasksController.AddNewTask(task);

            if (!taskAdded)
            {
                errorProvider.SetError(cbComics, "A task for this comic is already running or paused.");
                return;
            }

            UpdateUserSettings();
            this.DialogResult = DialogResult.OK;
        }

        private void numComicsToDownload_Click(object sender, EventArgs e)
        {
            rbDownloadLast.Checked = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Settings.Default.Reload();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            if (folderBrowser.ShowDialog() == DialogResult.OK)
                txtDownloadFolder.Text = folderBrowser.SelectedPath;
        }
        #endregion

        #region Helper Methods
        private void UpdateUserSettings()
        {
            Settings.Default.LastUsedComicInfoFile = ((ComicInfo)cbComics.SelectedValue).ComicInfoFile;

            if (rbDownloadOnlyNew.Checked)
                Settings.Default.LastNumberOfComicsToDownload = -1;
            else
                Settings.Default.LastNumberOfComicsToDownload = (long)numComicsToDownload.Value;

            Settings.Default.Save();
        }

        private void UpdateDownloadFolder()
        {
            ComicInfo comicInfo = (ComicInfo)cbComics.SelectedValue;
            txtDownloadFolder.Text = Path.Combine(Settings.Default.DefaultDownloadFolder, comicInfo.FriendlyName);
        }
        #endregion

        #region Events - cbComics
        private void cbComics_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDownloadFolder();
        } 
        #endregion                

        #region Events - chkOverrideStartUrl
        private void chkOverrideStartUrl_CheckedChanged(object sender, EventArgs e)
        {
            txtOverrideStartUrl.Enabled = chkOverrideStartUrl.Checked;
        } 
        #endregion
    }
}