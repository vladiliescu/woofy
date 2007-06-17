using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.Core
{
    public class DownloadFileCompletedEventArgs : EventArgs
    {
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
        /// <param name="fileAlreadyDownloaded">True if the comic was already downloaded, false otherwise.</param>
        public DownloadFileCompletedEventArgs(bool fileAlreadyDownloaded)
        {
            _fileAlreadyDownloaded = fileAlreadyDownloaded;
        }
    }
}
