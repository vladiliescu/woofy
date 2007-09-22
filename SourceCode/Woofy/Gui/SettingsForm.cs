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
            if (!string.IsNullOrEmpty(Woofy.Properties.Settings.Default.ProxyAddress))
            {
                txtProxyAddress.Text = Woofy.Properties.Settings.Default.ProxyAddress;
                txtProxyPort.Text = Woofy.Properties.Settings.Default.ProxyPort.ToString();

                chkUseProxy.Checked = true;
            }
        } 
        #endregion

        #region Events - clicks
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Woofy.Properties.Settings.Default.Reload();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(errorProvider.GetError(txtProxyPort)))
                return;

            if (chkUseProxy.Checked)
            {
                Woofy.Properties.Settings.Default.ProxyAddress = txtProxyAddress.Text;
                Woofy.Properties.Settings.Default.ProxyPort = int.Parse(txtProxyPort.Text);
            }
            else
            {
                Woofy.Properties.Settings.Default.ProxyAddress = string.Empty;
                Woofy.Properties.Settings.Default.ProxyPort = -1;
            }

            Woofy.Properties.Settings.Default.Save();

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

        #region Events - chkUseProxy
        private void chkUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            lblAddress.Enabled = 
                lblPort.Enabled = 
                txtProxyAddress.Enabled =
                txtProxyPort.Enabled = chkUseProxy.Checked;
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