using System;
using Woofy.Entities;

namespace Woofy.EventArguments
{
    public class DownloadedStripEventArgs : EventArgs
    {
       

        /// <summary>
        /// Gets the downloaded strip.
        /// </summary>
        public ComicStrip Strip { get; private set; }

        /// <summary>
        /// Gets the next page address. Can be null if the link next page hasn't been found.
        /// </summary>
        public Uri NextPageAddress { get; private set; }

        /// <summary>
        /// Gets the definition that will override the current one.
        /// </summary>
        public ComicDefinition OverridingDefinition { get; private set; }

        /// <summary>
        /// Used to determine whether the next page has been overriden by an event handler.
        /// </summary>
        public bool ComicIsOverriden { get; private set; }

        /// <summary>
        /// Use this to override the downloading comic.
        /// </summary>
        public void OverrideDownloadingComic(Uri nextPageAddress, ComicDefinition definition)
        {
            ComicIsOverriden = true;

            NextPageAddress = nextPageAddress;
            OverridingDefinition = definition;
        }

        public DownloadedStripEventArgs(ComicStrip strip, Uri nextPageAddress)
        {
            Strip = strip;
            NextPageAddress = nextPageAddress;
        }
    }
}
