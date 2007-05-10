using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.Core
{
    public interface IComicsDownloader
    {
        bool DownloadComic(string comicLink, string downloadDirectory);
    }
}
