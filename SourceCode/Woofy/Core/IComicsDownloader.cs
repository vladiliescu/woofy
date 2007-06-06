using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Woofy.Core
{
    public interface IComicsDownloader
    {
        bool DownloadComic(string comicLink, string downloadDirectory);

        bool DownloadComic(string comicLink, string downloadDirectory, WebProxy proxy);
    }
}
