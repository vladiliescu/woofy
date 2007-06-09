using System;
using System.Collections.Generic;
using System.Text;

using Woofy.Core;

namespace UnitTests
{
    internal class ComicsDownloaderStub : IComicsDownloader
    {
        private List<string> _comicLinks = new List<string>();
        public string[] ComicLinks
        {
            get { return _comicLinks.ToArray(); }
        }


        #region IComicsDownloader Members

        public void DownloadComic(string comicLink, out bool comicAlreadyDownloaded)
        {
            _comicLinks.Add(comicLink);

            comicAlreadyDownloaded = false;
        }

        public void DownloadComicAsync(string comicLink)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public event EventHandler<DownloadComicCompletedEventArgs> DownloadComicCompleted
        {
            add
            {
                //TODO: properly implement this.
                //throw new Exception("The method or operation is not implemented.");
            }
            remove
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public event EventHandler<DownloadedComicChunkEventArgs> DownloadedComicChunk
        {
            add
            {
                //throw new Exception("The method or operation is not implemented.");
            }
            remove
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion
    }
}
