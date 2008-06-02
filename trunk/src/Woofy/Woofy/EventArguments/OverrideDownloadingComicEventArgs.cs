using System;
using Woofy.Entities;

namespace Woofy.EventArguments
{
    public class OverrideDownloadingComicEventArgs : EventArgs
    {
        public Comic OverridingComic { get; private set; }
        public ComicStrip MostRecentStrip { get; private set; }
        public bool ComicHasFinishedDownloading { get; set; }
        public Comic DownloadingComic { get; private set; }

        public void OverrideComic(Comic comic, ComicStrip mostRecentStrip)
        {
            OverridingComic = comic;
            MostRecentStrip = mostRecentStrip;
        }

        public OverrideDownloadingComicEventArgs(Comic downloadingComic)
        {
            DownloadingComic = downloadingComic;
        }
    }
}