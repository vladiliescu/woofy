using System;
using System.Collections.Generic;
using System.Text;
using Woofy.Entities;

namespace Woofy.EventArguments
{
    public class DownloadedStripEventArgs : EventArgs
    {
        public ComicStrip Strip { get; private set; }

        public DownloadedStripEventArgs(ComicStrip strip)
        {
            Strip = strip;
        }
    }
}
