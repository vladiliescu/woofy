using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

using Woofy.Gui;
using Woofy.Properties;

namespace Woofy.Core
{
    public static class UpdateController
    {
        #region Static Members
        private static readonly string UpdatesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "updates");
        private static DownloadProgressForm _downloadProgressForm = null;
        private static FileDownloader _updateDownloader = new FileDownloader(UpdatesDirectory);
        private static bool _updateCheckInProgress = false;
        private static bool _initiatedByUser = false;
        private static MainForm _mainForm = null;
        #endregion

        #region .ctor
        static UpdateController()
        {
            _updateDownloader.DownloadFileCompleted += new EventHandler<DownloadFileCompletedEventArgs>(DownloadFileCompletedCallback);
            _updateDownloader.DownloadedFileChunk += new EventHandler<DownloadedFileChunkEventArgs>(DownloadedFileChunkCallback);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Asynchronously checks for updates.
        /// </summary>
        /// <param name="mainForm">A reference to the main form instance, in order to be able to run code on the main UI thread.</param>
        /// <param name="initiatedByUser">True if the update check was initiated by the user, false otherwise.</param>
        public static void CheckForUpdatesAsync(MainForm mainForm, bool initiatedByUser)
        {
            if (_updateCheckInProgress)
                return;

            _updateCheckInProgress = true;
            _mainForm = mainForm;
            _initiatedByUser = initiatedByUser;

            ThreadPool.QueueUserWorkItem(new WaitCallback(
                delegate
                {
                    string padUrl = Settings.Default.PadFileUrl;

                    FileDownloader fileDownloader = new FileDownloader(UpdatesDirectory);
                    fileDownloader.DownloadFileCompleted += new EventHandler<DownloadFileCompletedEventArgs>(DownloadPadCompleted);
                    fileDownloader.DownloadFileAsync(padUrl, null, true);
                }
            ));
        }
        #endregion

        #region Callback Methods
        /// <summary>
        /// Called when the PAD file has been downloaded. We can now see if there's an available update.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DownloadPadCompleted(object sender, DownloadFileCompletedEventArgs e)
        {
            PadDocument padDocument = new PadDocument(e.DownloadedFilePath);
            AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();
            Version previouslyRefusedApplicationVersion = new Version(Settings.Default.PreviouslyRefusedApplicationVersion);

            //there is no newer version
            if (padDocument.Version < assemblyName.Version)
            {
                if (_initiatedByUser)
                    MessageBox.Show("You are using the latest version of Woofy.", "Woofy", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _updateCheckInProgress = false;
                return;
            }

            //there is a newer version, but the user has refused to download it before, so we will only display the notification if the user manually checked for updates.
            if (!_initiatedByUser && padDocument.Version <= previouslyRefusedApplicationVersion)
            {
                _updateCheckInProgress = false;
                return;
            }

            string text = string.Format("A newer version of Woofy ({0}) has been released.\n Would you like to download and install it?", padDocument.Version.ToString());
            if (MessageBox.Show(text, "Woofy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                Settings.Default.PreviouslyRefusedApplicationVersion = padDocument.Version.ToString();
                Settings.Default.Save();

                _updateCheckInProgress = false;
                return;
            }

            DownloadAndInstallLatestVersionAsync(padDocument);
        }

        /// <summary>
        /// Called when an update file chunk has been downloaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DownloadedFileChunkCallback(object sender, DownloadedFileChunkEventArgs e)
        {
            _downloadProgressForm.IncrementProgress(e.BytesDownloaded);
        }

        /// <summary>
        /// Called when the update has been completely downloaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DownloadFileCompletedCallback(object sender, DownloadFileCompletedEventArgs e)
        {
            Process.Start(e.DownloadedFilePath);

            Application.Exit();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Shows the download progress form and starts to download the updates asynchronously.
        /// </summary>
        /// <param name="padDocument">The PAD document, containing details about the update.</param>
        private static void DownloadAndInstallLatestVersionAsync(PadDocument padDocument)
        {
            _mainForm.InitializeUpdatesDownloadProgressForm(padDocument.FileSizeInBytes / 1024);

            //get a reference to the download progress form, in order to be able to update the download progress
            DownloadProgressForm downloadProgressForm;
            foreach (Form form in Application.OpenForms)
            {
                downloadProgressForm = form as DownloadProgressForm;
                if (downloadProgressForm != null)
                {
                    _downloadProgressForm = downloadProgressForm;
                    break;
                }
            }

            _updateDownloader.DownloadFileAsync(padDocument.PrimaryDownloadUrl, null, true);
        }
        #endregion
    }
}
