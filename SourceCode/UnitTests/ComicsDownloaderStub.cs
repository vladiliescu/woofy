using System;
using System.Collections.Generic;

using Woofy.Core;

namespace UnitTests
{
    internal class ComicsDownloaderStub : IFileDownloader
    {
        private List<string> _comicLinks = new List<string>();
        public string[] ComicLinks
        {
            get { return _comicLinks.ToArray(); }
        }


        #region IComicsDownloader Members

        public void DownloadFile(string comicLink, out bool comicAlreadyDownloaded)
        {
            _comicLinks.Add(comicLink);

            comicAlreadyDownloaded = false;
        }

        public void DownloadFileAsync(string comicLink)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public event EventHandler<DownloadFileCompletedEventArgs> DownloadFileCompleted
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

        public event EventHandler<DownloadedFileChunkEventArgs> DownloadedFileChunk
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

        public void DownloadFileAsync(string fileLink, string referrer)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
