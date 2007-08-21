using System;
using System.Collections.Generic;

namespace Woofy.Core
{
    public class CountingFileDownloader : IFileDownloader
    {
        private List<string> comicLinks = new List<string>();
        public string[] ComicLinks
        {
            get { return comicLinks.ToArray(); }
        }

        #region IFileDownloader Members

        public void DownloadFile(string comicLink, string referrer, out bool comicAlreadyDownloaded)
        {
            comicLinks.Add(comicLink);

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
