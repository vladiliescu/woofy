using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.Core
{
    public class DownloadedFileChunkEventArgs : EventArgs
    {
        private bool _cancel = false;
        /// <summary>
        /// Gets or sets whether the download should be cancelled.
        /// </summary>
        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }

        private int _bytesDownloaded;
        /// <summary>
        /// The number of downloaded bytes.
        /// </summary>
        public int BytesDownloaded
        {
            get { return _bytesDownloaded; }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DownloadedFileChunkEventArgs"/>.
        /// </summary>
        /// <param name="bytesDownloaded">Number of downloaded bytes.</param>
        public DownloadedFileChunkEventArgs(int bytesDownloaded)
        {
            _bytesDownloaded = bytesDownloaded;
        }
    }
}
