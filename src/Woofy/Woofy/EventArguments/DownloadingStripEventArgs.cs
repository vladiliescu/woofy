using System;
using System.Collections.Generic;
using System.Text;
using Woofy.Entities;

namespace Woofy.EventArguments
{
    public class DownloadingStripEventArgs : EventArgs
    {
        public bool AbortDownload { get; set; }
        public bool SkipStrip { get; set; }
        public ComicStrip Strip { get; private set; }

        public DownloadingStripEventArgs(ComicStrip strip)
        {
            Strip = strip;
        }
    }
}
