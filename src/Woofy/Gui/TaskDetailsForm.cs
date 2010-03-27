using System;
using System.Windows.Forms;
using System.IO;
using Woofy.Core;
using Woofy.Settings;

namespace Woofy.Gui
{
    public partial class TaskDetailsForm : Form
    {
        #region Instance Members
        private readonly ComicTasksController tasksController;
        #endregion

        #region .ctor
        public TaskDetailsForm(ComicTasksController tasksController)
        {
            InitializeComponent();

            this.tasksController = tasksController;
        }
        #endregion

		

    	#region Events - Form
        private void TaskDetails_Load(object sender, EventArgs e)
        {
			ShowOrHideAdvancedOptions(UserSettings.ShowAdvancedComicOptions);

            cbComics.DataSource = ComicDefinition.GetAvailableComicDefinitions();

            if (!string.IsNullOrEmpty(UserSettings.LastUsedComicDefinitionFile))
            {
                int i = 0;
                foreach (ComicDefinition comicInfo in cbComics.Items)
                {
                    if (comicInfo.ComicInfoFile.Equals(UserSettings.LastUsedComicDefinitionFile, StringComparison.OrdinalIgnoreCase))
                    {
                        cbComics.SelectedIndex = i;
                        break;
                    }

                    i++;
                }
            }

            if (UserSettings.LastNumberOfComicsToDownload.HasValue)
            {
                rbDownloadLast.Checked = true;
                numComicsToDownload.Value = (decimal)UserSettings.LastNumberOfComicsToDownload;
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

            ComicDefinition comicInfo = (ComicDefinition)cbComics.SelectedItem;
            long? comicsToDownload;
            if (rbDownloadLast.Checked)
                comicsToDownload = (long)numComicsToDownload.Value;
            else
                comicsToDownload = null;
            string downloadFolder = txtDownloadFolder.Text;
            string startUrl = chkOverrideStartUrl.Checked ? txtOverrideStartUrl.Text : comicInfo.StartUrl;

            ComicTask task = new ComicTask(comicInfo.FriendlyName, comicInfo.ComicInfoFile, comicsToDownload, downloadFolder, startUrl, chkRandomPauses.Checked);
            bool taskAdded = tasksController.AddNewTask(task);

            if (!taskAdded)
            {
                notificationToolTip.Show("A task for this comic has already been added.", btnOk);
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
            UserSettings.LoadData();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            if (folderBrowser.ShowDialog() == DialogResult.OK)
                txtDownloadFolder.Text = folderBrowser.SelectedPath;
        }

		private void OnAdvancedOptionsCheckedChanged(object sender, EventArgs e)
		{
			ShowOrHideAdvancedOptions(chkAdvancedOptions.Checked);
		}

        #endregion

        #region Helper Methods
        private void UpdateUserSettings()
        {
            UserSettings.LastUsedComicDefinitionFile = ((ComicDefinition)cbComics.SelectedValue).ComicInfoFile;
			UserSettings.ShowAdvancedComicOptions = chkAdvancedOptions.Checked;

            if (rbDownloadOnlyNew.Checked)
                UserSettings.LastNumberOfComicsToDownload = null;
            else
                UserSettings.LastNumberOfComicsToDownload = (long)numComicsToDownload.Value;

            UserSettings.SaveData();
        }

		private void ShowOrHideAdvancedOptions(bool show)
		{
			gbAdvanced.Visible = show;
			Height = show ? 325 : 205;
			chkAdvancedOptions.Checked = show;
		}

        private void UpdateDownloadFolder()
        {
            ComicDefinition comicInfo = (ComicDefinition)cbComics.SelectedValue;
            txtDownloadFolder.Text = Path.Combine(UserSettings.DefaultDownloadFolder ?? "", comicInfo.FriendlyName);
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