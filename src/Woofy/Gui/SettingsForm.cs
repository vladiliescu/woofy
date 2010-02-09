using System;
using System.ComponentModel;
using System.Windows.Forms;

using Woofy.Settings;

namespace Woofy.Gui
{
    public partial class SettingsForm : Form
    {
        #region .ctor
        public SettingsForm()
        {
            InitializeComponent();

            InitControls();
        }
        #endregion

        #region Helper Methods
        private void InitControls()
        {
            txtDefaultDownloadFolder.Text = UserSettings.DefaultDownloadFolder;
            chkAutomaticallyCheckForUpdates.Checked = UserSettings.AutomaticallyCheckForUpdates;
            chkMinimizeToTray.Checked = UserSettings.MinimizeToTray;
			chkCloseWhenAllComicsHaveFinished.Checked = UserSettings.CloseWhenAllComicsHaveFinished;

            if (!string.IsNullOrEmpty(UserSettings.ProxyAddress))
            {
                txtProxyAddress.Text = UserSettings.ProxyAddress;
                txtProxyPort.Text = UserSettings.ProxyPort.ToString();

                chkUseProxy.Checked = true;
            }

            if (!string.IsNullOrEmpty(UserSettings.ProxyUsername))
            {
                txtUsername.Text = UserSettings.ProxyUsername;
                txtPassword.Text = UserSettings.ProxyPassword;

                chkUseCredentials.Checked = true;
            }

        }
        #endregion

        #region Events - clicks
        private void btnCancel_Click(object sender, EventArgs e)
        {
            UserSettings.LoadData();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(errorProvider.GetError(txtProxyPort)))
                return;

            if (chkUseProxy.Checked)
            {
                UserSettings.ProxyAddress = txtProxyAddress.Text;
                
                int tempProxyPort;
                if (int.TryParse(txtProxyPort.Text, out tempProxyPort))
                    UserSettings.ProxyPort = tempProxyPort;
                else 
                    UserSettings.ProxyPort = null;
            }
            else
            {
                UserSettings.ProxyAddress = string.Empty;
                UserSettings.ProxyPort = null;
            }

            if (chkUseCredentials.Checked)
            {
                UserSettings.ProxyUsername = txtUsername.Text;
                UserSettings.ProxyPassword = txtPassword.Text;
            }
            else
            {
                UserSettings.ProxyUsername = string.Empty;
                UserSettings.ProxyPassword = string.Empty;
            }


            UserSettings.DefaultDownloadFolder = txtDefaultDownloadFolder.Text;
            UserSettings.AutomaticallyCheckForUpdates = chkAutomaticallyCheckForUpdates.Checked;
            UserSettings.MinimizeToTray = chkMinimizeToTray.Checked;
			UserSettings.CloseWhenAllComicsHaveFinished = chkCloseWhenAllComicsHaveFinished.Checked;

            UserSettings.SaveData();

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

        #region Events - Checked Changed
        private void chkUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            lblAddress.Enabled =
                lblPort.Enabled =
                txtProxyAddress.Enabled =
                txtProxyPort.Enabled = chkUseProxy.Checked;
        }

        private void chkUseCredentials_CheckedChanged(object sender, EventArgs e)
        {
            lblUsername.Enabled =
                lblPassword.Enabled =
                txtUsername.Enabled =
                txtPassword.Enabled = chkUseCredentials.Checked;
        }

        #endregion

        #region Events - Validation
        private void txtProxyPort_Validating(object sender, CancelEventArgs e)
        {
            int tempValue;
            if (!int.TryParse(txtProxyPort.Text, out tempValue))
                errorProvider.SetError(txtProxyPort, "The proxy port must be a valid integer value.");
            else
                errorProvider.SetError(txtProxyPort, null);
        }
        #endregion

        
    }
}