using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Woofy.Core
{
    public interface IFileDownloader
    {
        void DownloadFile(string comicLink, out bool comicAlreadyDownloaded);

        void DownloadFileAsync(string comicLink);

        event EventHandler<DownloadFileCompletedEventArgs> DownloadFileCompleted;

        event EventHandler<DownloadedFileChunkEventArgs> DownloadedFileChunk;
    }
}
