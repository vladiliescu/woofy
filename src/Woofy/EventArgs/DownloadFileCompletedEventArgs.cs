using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.Core
{
    public class DownloadFileCompletedEventArgs : EventArgs
    {
        private string _downloadedFilePath;
        /// <summary>
        /// Gets the path to the downloaded file.
        /// </summary>
        public string DownloadedFilePath
        {
            get { return _downloadedFilePath; }
        }

        private bool _fileAlreadyDownloaded;
        /// <summary>
        /// Gets whether the file was already downloaded.
        /// </summary>
        public bool FileAlreadyDownloaded
        {
            get { return _fileAlreadyDownloaded; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadFileCompletedEventArgs"/>.
        /// </summary>
        /// <param name="downloadedFilePath">Path to the downloaded file.</param>
        /// <param name="fileAlreadyDownloaded">True if the comic was already downloaded, false otherwise.</param>
        public DownloadFileCompletedEventArgs(string downloadedFilePath, bool fileAlreadyDownloaded)
        {
            _downloadedFilePath = downloadedFilePath;
            _fileAlreadyDownloaded = fileAlreadyDownloaded;
        }
    }
}
