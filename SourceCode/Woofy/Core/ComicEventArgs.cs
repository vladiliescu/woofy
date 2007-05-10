using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.Core
{
    public class ComicEventArgs : EventArgs
    {
        private int _downloadedComics;
        /// <summary>
        /// Number of comics downloaded for this particular comic.
        /// </summary>
        public int DownloadedComics
        {
            get { return _downloadedComics; }
        }

        private string _currentUrl;
        /// <summary>
        /// The page where we left the download at.
        /// </summary>
        public string CurrentUrl
        {
            get { return _currentUrl; }
        }


        public ComicEventArgs(int downloadedComics, string currentUrl)
        {
            _downloadedComics = downloadedComics;
            _currentUrl = currentUrl;
        }	
    }
}
