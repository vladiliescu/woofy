using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.Core
{
    public class DownloadComicCompletedEventArgs : EventArgs
    {
        private bool _comicAlreadyDownloaded;
        /// <summary>
        /// Gets whether the comic was already downloaded.
        /// </summary>
        public bool ComicAlreadyDownloaded
        {
            get { return _comicAlreadyDownloaded; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadComicCompletedEventArgs"/>.
        /// </summary>
        /// <param name="comicAlreadyDownloaded">True if the comic was already downloaded, false otherwise.</param>
        public DownloadComicCompletedEventArgs(bool comicAlreadyDownloaded)
        {
            _comicAlreadyDownloaded = comicAlreadyDownloaded;
        }
    }
}
