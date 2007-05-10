using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Woofy.Properties;

namespace Woofy.Gui
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        #region Events - clicks
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Settings.Default.Reload();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            //folderBrowser.Description = "Select the default folder in which the comics will be downloaded.";

            if (folderBrowser.ShowDialog() == DialogResult.OK)
                txtDefaultDownloadFolder.Text = folderBrowser.SelectedPath;
        } 
        #endregion
    }
}