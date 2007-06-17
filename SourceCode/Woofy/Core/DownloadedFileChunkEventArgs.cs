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
    }
}
