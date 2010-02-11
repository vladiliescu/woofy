using System;
using System.Threading;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using Woofy.Core;
using Woofy.Settings;
using Woofy.Gui;

namespace Woofy.Updates
{
    public class UpdateManager
    {
        private static bool isRunning = false;
        private static bool initiatedByUser;
        private static MainForm mainForm;
        private static DownloadProgressForm downloadProgressForm;
        private static readonly FileDownloader fileDownloader = new FileDownloader(Path.GetTempPath());

        #region .ctor
        static UpdateManager()
        {
            fileDownloader.DownloadFileCompleted += new EventHandler<DownloadFileCompletedEventArgs>(OnFileDownloaded);
            fileDownloader.DownloadedFileChunk += new EventHandler<DownloadedFileChunkEventArgs>(OnFileChunkDownloaded);
        }        
        #endregion

        public static void CheckForUpdatesAsync(bool userInitiated, MainForm parentForm)
        {
            if (isRunning)
                return;

            initiatedByUser = userInitiated;
            mainForm = parentForm;

            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(
                delegate
                {
                    CheckForUpdates();
                }
            ), null);
        }

        private static void CheckForUpdates()
        {
            WebRequest request = WebConnectionFactory.GetNewWebRequestInstance(AppSettings.UpdateDescriptionFileAddress);
            Stream responseStream;

            try
            {
                responseStream = request.GetResponse().GetResponseStream();
            }
            catch (WebException ex)
            {
                Logger.LogException(ex);
                if (initiatedByUser)
                    mainForm.DisplayMessageBox("Unable to retrieve update information.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UpdateDescription updateDescription = new UpdateDescription(responseStream);

            Release release = GetReleaseToUpgradeTo(updateDescription);
            if (release == null)
            {
                if (initiatedByUser)
                    mainForm.DisplayMessageBox("No updates are available at this time.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DoCleanup();
                return;
            }

            if (release == updateDescription.Woofy[0])
            {
                UserSettings.LastReportedWoofyVersion = release.VersionNumber;
                UserSettings.SaveData();
                if (mainForm.DisplayMessageBox(GetNewVersionText("Woofy", release), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    DoCleanup();
                    return;
                }
            }

            UpgradeToRelease(release);
        }

        private static void UpgradeToRelease(Release release)
        {
            mainForm.InitializeUpdatesDownloadProgressForm(release.Size / 1024);

            //get a reference to the download progress form, in order to be able to update the download progress
            foreach (Form form in Application.OpenForms)
            {
                DownloadProgressForm progressForm = form as DownloadProgressForm;
                if (progressForm != null)
                {
                    downloadProgressForm = progressForm;
                    break;
                }
            }

            fileDownloader.DownloadFileAsync(release.DownloadAddress, null, true, null);
        }

        private static void DoCleanup()
        {
            isRunning = false;
        }

        public static Release GetReleaseToUpgradeTo(UpdateDescription updateDescription)
        {
            Release latestWoofyRelease = updateDescription.Woofy[0];

            if (IsUpgradeCandidate(latestWoofyRelease, AppSettings.VersionNumber, UserSettings.LastReportedWoofyVersion, initiatedByUser))
                return latestWoofyRelease;
            else 
                return null;
        }

        private static bool IsUpgradeCandidate(Release release, string applicationVersionNumber, string lastReportedUpgradeVersion, bool initiatedByUser)
        {
            if (!release.IsNewerThanVersion(applicationVersionNumber))
                return false;

            if (release.VersionNumber == lastReportedUpgradeVersion && !initiatedByUser)
                return false;

            return true;
        }

        private static string GetNewVersionText(string product, Release release)
        {
            string releaseDate = release.ReleaseDate.HasValue ? release.ReleaseDate.Value.ToString("d") : "";
            return string.Format("A newer {0} version ({1}) has been released on {2}. Would you like to upgrade?", product, release.VersionNumber, releaseDate);
        }

        #region Callbacks
        /// <summary>
        /// Called when an update file chunk has been downloaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnFileChunkDownloaded(object sender, DownloadedFileChunkEventArgs e)
        {
            downloadProgressForm.IncrementProgress(e.BytesDownloaded);
        }

        /// <summary>
        /// Called when the update has been completely downloaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnFileDownloaded(object sender, DownloadFileCompletedEventArgs e)
        {
            Process.Start(e.DownloadedFilePath);

            Application.Exit();
        }
        #endregion
    }
}
