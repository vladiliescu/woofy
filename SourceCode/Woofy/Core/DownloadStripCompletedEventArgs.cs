using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.Core
{
    public class DownloadStripCompletedEventArgs : EventArgs
    {
        private int _downloadedComics;
        /// <summary>
        /// Number of downloaded comic strips.
        /// </summary>
        public int DownloadedComics
        {
            get { return _downloadedComics; }
        }

        private string _currentUrl;
        /// <summary>
        /// The page containing the current strip.
        /// </summary>
        public string CurrentUrl
        {
            get { return _currentUrl; }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DownloadSingleComicCompletedEventArgs" />.
        /// </summary>
        /// <param name="downloadedComics">The number of downloaded comic strips.</param>
        /// <param name="currentUrl">The page containing the current strip.</param>
        public DownloadStripCompletedEventArgs(int downloadedComics, string currentUrl)
        {
            _downloadedComics = downloadedComics;
            _currentUrl = currentUrl;
        }	
    }
}
