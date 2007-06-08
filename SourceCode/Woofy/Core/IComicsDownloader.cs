using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Woofy.Core
{
    public interface IComicsDownloader
    {
        void DownloadComic(string comicLink, out bool comicAlreadyDownloaded);

        void DownloadComicAsync(string comicLink);

        event EventHandler<DownloadComicCompletedEventArgs> DownloadComicCompleted;
    }
}
