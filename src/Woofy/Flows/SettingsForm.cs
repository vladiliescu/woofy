using System;
using System.Windows.Forms;
using Woofy.Core;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows
{
    public partial class SettingsForm : Form
    {
        private readonly IUserSettings settings = ContainerAccessor.Resolve<IUserSettings>();

        public SettingsForm()
        {
            InitializeComponent();

            InitControls();
        }

        private void InitControls()
        {
            txtDefaultDownloadFolder.Text = settings.DownloadFolder;
            chkAutomaticallyCheckForUpdates.Checked = settings.AutomaticallyCheckForUpdates;
            chkMinimizeToTray.Checked = settings.MinimizeToTray;
        }

        private void OnCancel(object sender, EventArgs e)
        {
            settings.Load();
            DialogResult = DialogResult.Cancel;
        }

        private void OnOk(object sender, EventArgs e)
        {
            settings.DownloadFolder = txtDefaultDownloadFolder.Text;
            settings.AutomaticallyCheckForUpdates = chkAutomaticallyCheckForUpdates.Checked;
            settings.MinimizeToTray = chkMinimizeToTray.Checked;

            settings.Save();

            DialogResult = DialogResult.OK;
        }

        private void OnBrowse(object sender, EventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog();
            //folderBrowser.Description = "Select the default folder in which the comics will be downloaded.";

            if (folderBrowser.ShowDialog() == DialogResult.OK)
                txtDefaultDownloadFolder.Text = folderBrowser.SelectedPath;
        }
    }
}