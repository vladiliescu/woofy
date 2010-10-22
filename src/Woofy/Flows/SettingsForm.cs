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
            txtDefaultDownloadFolder.Text = UserSettingsOld.DefaultDownloadFolder;
            chkAutomaticallyCheckForUpdates.Checked = UserSettingsOld.AutomaticallyCheckForUpdates;
            chkMinimizeToTray.Checked = UserSettingsOld.MinimizeToTray;
			chkCloseWhenAllComicsHaveFinished.Checked = UserSettingsOld.CloseWhenAllComicsHaveFinished;

            if (!string.IsNullOrEmpty(UserSettingsOld.ProxyAddress))
            {
                txtProxyAddress.Text = UserSettingsOld.ProxyAddress;
                txtProxyPort.Text = UserSettingsOld.ProxyPort.ToString();

                chkUseProxy.Checked = true;
            }

            if (!string.IsNullOrEmpty(UserSettingsOld.ProxyUsername))
            {
                txtUsername.Text = UserSettingsOld.ProxyUsername;
                txtPassword.Text = UserSettingsOld.ProxyPassword;

                chkUseCredentials.Checked = true;
            }

        }
        #endregion

        #region Events - clicks
        private void btnCancel_Click(object sender, EventArgs e)
        {
            UserSettingsOld.LoadData();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(errorProvider.GetError(txtProxyPort)))
                return;

            if (chkUseProxy.Checked)
            {
                UserSettingsOld.ProxyAddress = txtProxyAddress.Text;
                
                int tempProxyPort;
                if (int.TryParse(txtProxyPort.Text, out tempProxyPort))
                    UserSettingsOld.ProxyPort = tempProxyPort;
                else 
                    UserSettingsOld.ProxyPort = null;
            }
            else
            {
                UserSettingsOld.ProxyAddress = string.Empty;
                UserSettingsOld.ProxyPort = null;
            }

            if (chkUseCredentials.Checked)
            {
                UserSettingsOld.ProxyUsername = txtUsername.Text;
                UserSettingsOld.ProxyPassword = txtPassword.Text;
            }
            else
            {
                UserSettingsOld.ProxyUsername = string.Empty;
                UserSettingsOld.ProxyPassword = string.Empty;
            }


            UserSettingsOld.DefaultDownloadFolder = txtDefaultDownloadFolder.Text;
            UserSettingsOld.AutomaticallyCheckForUpdates = chkAutomaticallyCheckForUpdates.Checked;
            UserSettingsOld.MinimizeToTray = chkMinimizeToTray.Checked;
			UserSettingsOld.CloseWhenAllComicsHaveFinished = chkCloseWhenAllComicsHaveFinished.Checked;

            UserSettingsOld.SaveData();

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