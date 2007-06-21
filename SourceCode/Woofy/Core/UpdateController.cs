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
        #endregion

        #region Public Methods
        /// <summary>
        /// Asynchronously checks for updates.
        /// </summary>
        public static void CheckForUpdatesAsync()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(
                delegate
                {
                    string padUrl = Settings.Default.PadFileUrl;

                    FileDownloader fileDownloader = new FileDownloader(UpdatesDirectory);
                    fileDownloader.DownloadFileCompleted += new EventHandler<DownloadFileCompletedEventArgs>(DownloadPadCompleted);
                    fileDownloader.DownloadFileAsync(padUrl);
                }
            ));
        } 
        #endregion

        #region Helper Methods
        /// <summary>
        /// Shows the download progress form and starts to download the updates asynchronously.
        /// </summary>
        /// <param name="padDocument">The PAD document, containing details about the update.</param>
        private static void DownloadAndInstallLatestVersionAsync(PadDocument padDocument)
        {
            _downloadProgressForm = new DownloadProgressForm(padDocument.FileSizeInBytes / 1024);

            FileDownloader updateDownloader = new FileDownloader(UpdatesDirectory);
            updateDownloader.DownloadFileCompleted += new EventHandler<DownloadFileCompletedEventArgs>(DownloadFileCompletedCallback);
            updateDownloader.DownloadedFileChunk += new EventHandler<DownloadedFileChunkEventArgs>(DownloadedFileChunkCallback);
            updateDownloader.DownloadFileAsync(padDocument.PrimaryDownloadUrl);

            _downloadProgressForm.Show();

            //hide the main form
            foreach (Form form in Application.OpenForms)
            {
                if (form is MainForm)
                    form.Visible = false;
            }

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
            Version latestApplicationVersion = new Version(Settings.Default.LatestApplicationVersion);

            //only offer to download the update if it's a new update, and if the user hasn't been asked about this before
            if (padDocument.Version < assemblyName.Version)//TODO: && latestApplicationVersion > assemblyName.Version)
                return;

            string text = string.Format("A newer version of Woofy ({0}) has been released. Would you like to download and install it?", padDocument.Version.ToString());
            if (MessageBox.Show(text, "Woofy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

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
    }
}
