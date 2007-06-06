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

        public bool DownloadComic(string comicLink, string downloadDirectory)
        {
            return DownloadComic(comicLink, downloadDirectory, null);
        }

        public bool DownloadComic(string comicLink, string downloadDirectory, System.Net.WebProxy proxy)
        {
            _comicLinks.Add(comicLink);

            return true;
        }

        #endregion
    }
}
