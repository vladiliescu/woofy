using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Woofy.Core
{
    public interface IFileDownloader
    {
        string DownloadFile(string comicLink, string referrer, string fileName, out bool comicAlreadyDownloaded);

        void DownloadFileAsync(string comicLink);

        void DownloadFileAsync(string fileLink, string referrer);

        event EventHandler<DownloadFileCompletedEventArgs> DownloadFileCompleted;

        event EventHandler<DownloadedFileChunkEventArgs> DownloadedFileChunk;
    }
}
