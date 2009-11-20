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
            txtDefaultDownloadFolder.Text = UsrSettings.DefaultDownloadFolder;
            chkAutomaticallyCheckForUpdates.Checked = UsrSettings.AutomaticallyCheckForUpdates;
            chkMinimizeToTray.Checked = UsrSettings.MinimizeToTray;

            if (!string.IsNullOrEmpty(UsrSettings.ProxyAddress))
            {
                txtProxyAddress.Text = UsrSettings.ProxyAddress;
                txtProxyPort.Text = UsrSettings.ProxyPort.ToString();

                chkUseProxy.Checked = true;
            }

            if (!string.IsNullOrEmpty(UsrSettings.ProxyUsername))
            {
                txtUsername.Text = UsrSettings.ProxyUsername;
                txtPassword.Text = UsrSettings.ProxyPassword;

                chkUseCredentials.Checked = true;
            }

        }
        #endregion

        #region Events - clicks
        private void btnCancel_Click(object sender, EventArgs e)
        {
            UsrSettings.LoadData();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(errorProvider.GetError(txtProxyPort)))
                return;

            if (chkUseProxy.Checked)
            {
                UsrSettings.ProxyAddress = txtProxyAddress.Text;
                
                int tempProxyPort;
                if (int.TryParse(txtProxyPort.Text, out tempProxyPort))
                    UsrSettings.ProxyPort = tempProxyPort;
                else 
                    UsrSettings.ProxyPort = null;
            }
            else
            {
                UsrSettings.ProxyAddress = string.Empty;
                UsrSettings.ProxyPort = null;
            }

            if (chkUseCredentials.Checked)
            {
                UsrSettings.ProxyUsername = txtUsername.Text;
                UsrSettings.ProxyPassword = txtPassword.Text;
            }
            else
            {
                UsrSettings.ProxyUsername = string.Empty;
                UsrSettings.ProxyPassword = string.Empty;
            }


            UsrSettings.DefaultDownloadFolder = txtDefaultDownloadFolder.Text;
            UsrSettings.AutomaticallyCheckForUpdates = chkAutomaticallyCheckForUpdates.Checked;
            UsrSettings.MinimizeToTray = chkMinimizeToTray.Checked;

            UsrSettings.SaveData();

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