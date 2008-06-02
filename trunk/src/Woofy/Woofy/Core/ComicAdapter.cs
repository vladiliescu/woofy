using System;
using Woofy.Entities;
using Woofy.Other;
using System.IO;
using System.Net;
using Woofy.EventArguments;

namespace Woofy.Core
{
    public class ComicAdapter
    {
        private readonly FileDownloadService _fileDownloadService = new FileDownloadService();
        private readonly PageParseService _pageParseService = new PageParseService();
        private readonly FileWrapper _file = new FileWrapper();
        private readonly PathWrapper _path = new PathWrapper();
        private readonly WebClientWrapper _webClient = new WebClientWrapper();


        private Uri GetComicStartAddress(ComicDefinition definition, ComicStrip mostRecentStrip)
        {
            if (mostRecentStrip == null)
                return _pageParseService.GetLatestPageOrStartAddress(definition.HomePageAddress, definition.LatestIssueRegex);

            string pageContent = _webClient.DownloadString(mostRecentStrip.SourcePageAddress);
            Uri[] nextStripLinks = _pageParseService.RetrieveLinksFromPageByRegex(definition.NextPageRegex, pageContent, mostRecentStrip.SourcePageAddress);
            if (nextStripLinks.Length == 0)
                return null;

            return nextStripLinks[0];
        }


        private Uri GetFirstAvailableStartAddress(ComicDefinition definition, ComicStrip mostRecentStrip)
        {
            Uri currentAddress;
            OverrideDownloadingComicEventArgs e;
            do
            {
                currentAddress = GetComicStartAddress(definition, mostRecentStrip);
                if (currentAddress != null)
                    break;

                e = OnOverrideDownloadingComic(definition.Comic, true);
                if (e.OverridingComic == null)
                    break;

                definition = e.OverridingComic.Definition;
                mostRecentStrip = e.MostRecentStrip;
            }
            while (true);

            return currentAddress;
        }


        public void CheckComicForUpdates(Comic comic, ComicStrip mostRecentStrip)
        {
            var definition = comic.Definition;
            var downloadFolder = _path.Combine(ApplicationSettings.DefaultDownloadFolder, comic.Name);
            if (!Directory.Exists(downloadFolder))
                Directory.CreateDirectory(downloadFolder);

            try
            {
                OverrideDownloadingComicEventArgs e;
                do
                {
                    var currentAddress = GetFirstAvailableStartAddress(definition, mostRecentStrip);
                    if (currentAddress == null)
                        return;


                    DownloadStrips(definition, currentAddress, downloadFolder, out e);

                    if (e.OverridingComic == null)
                        break;

                    definition = e.OverridingComic.Definition;
                    mostRecentStrip = e.MostRecentStrip;
                }
                while (true);
                
            }
            catch (UriFormatException ex)
            {
            }
            catch (WebException ex)
            {
                //TODO: trebuie sa raportez exceptiile ca erori.
            }
        }

        private void DownloadStrips(ComicDefinition definition, Uri currentAddress, string downloadFolder, out OverrideDownloadingComicEventArgs e)
        {
            e = new OverrideDownloadingComicEventArgs(null);
            do
            {
                var pageContent = _webClient.DownloadString(currentAddress);

                var stripAddresses = _pageParseService.RetrieveLinksFromPageByRegex(definition.StripRegex, pageContent, currentAddress);
                var nextPageAddresses = _pageParseService.RetrieveLinksFromPageByRegex(definition.NextPageRegex, pageContent, currentAddress);
                var nextPageAddress = (nextPageAddresses.Length == 0 ? null : nextPageAddresses[0]);

                if (!MatchedLinksObeyRules(stripAddresses.Length, definition.AllowMissingStrips, definition.AllowMultipleStrips))//, ref downloadOutcome))
                {
                    //nextAddress = null;
                    break;
                }

                foreach (var stripAddress in stripAddresses)
                {
                    var stripFileName = stripAddress.AbsoluteUri.Substring(stripAddress.AbsoluteUri.LastIndexOf('/') + 1);
                    var downloadPath = _path.Combine(downloadFolder, stripFileName);

                    var strip = new ComicStrip(definition.Comic)
                                    {
                                        SourcePageAddress = currentAddress,
                                        FilePath = downloadPath
                                    };

                    _fileDownloadService.DownloadFile(stripAddress, downloadPath, currentAddress);

                    OnDownloadedStrip(strip, nextPageAddress);
                    e = OnOverrideDownloadingComic(definition.Comic, nextPageAddress == null);

                    if (e.OverridingComic != null)
                        return;
                }
                    
                currentAddress = nextPageAddress;
            }
            while (currentAddress != null);
        }


        private bool MatchedLinksObeyRules(int linksLength, bool allowMissingStrips, bool allowMultipleStrips)//, ref DownloadOutcome downloadOutcome)
        {
            if (linksLength == 0 && !allowMissingStrips)
            {
                //downloadOutcome = DownloadOutcome.NoStripMatchesRuleBroken;
                return false;
            }

            if (linksLength > 1 && !allowMultipleStrips)
            {
                //downloadOutcome = DownloadOutcome.MultipleStripMatchesRuleBroken;
                return false;
            }

            return true;
        }

        public void RefreshComicFavicon(Comic comic)
        {
            Uri faviconAddress = _pageParseService.RetrieveFaviconAddressFromPage(comic.Definition.HomePageAddress);
            if (faviconAddress == null)
                return;

            string faviconTempPath = _path.GetTempFileName();
            _webClient.DownloadFile(faviconAddress, faviconTempPath);

            //TODO: poate ar trebui sa folosesc getFileExtension
            string faviconName = comic.Id.ToString() + ".ico";
            string faviconPath = _path.Combine(ApplicationSettings.FaviconsFolder, faviconName);

            _file.Delete(faviconPath);
            _file.Move(faviconTempPath, faviconPath);
        }


        #region Event - DownloadingStrip
        private event EventHandler<DownloadingStripEventArgs> _downloadingStrip;
        public event EventHandler<DownloadingStripEventArgs> DownloadingStrip
        {
            add { _downloadingStrip += value; }
            remove { _downloadingStrip -= value; }
        }

        protected virtual void OnDownloadingStrip(DownloadingStripEventArgs e)
        {
            EventHandler<DownloadingStripEventArgs> reference = _downloadingStrip;
            if (reference != null)
                reference(this, e);
        }

        private DownloadingStripEventArgs OnDownloadingStrip(ComicStrip strip)
        {
            DownloadingStripEventArgs e = new DownloadingStripEventArgs(strip);
            OnDownloadingStrip(e);

            return e;
        }
        #endregion

        #region Event - DownloadedStrip
        private event EventHandler<DownloadedStripEventArgs> _downloadedStrip;
        public event EventHandler<DownloadedStripEventArgs> DownloadedStrip
        {
            add { _downloadedStrip += value; }
            remove { _downloadedStrip -= value; }
        }

        protected virtual void OnDownloadedStrip(DownloadedStripEventArgs e)
        {
            EventHandler<DownloadedStripEventArgs> reference = _downloadedStrip;
            if (reference != null)
                reference(this, e);
        }

        private DownloadedStripEventArgs OnDownloadedStrip(ComicStrip strip, Uri nextPageAddress)
        {
            var e = new DownloadedStripEventArgs(strip, nextPageAddress);
            OnDownloadedStrip(e);

            return e;
        }
        #endregion

        #region Event - OverrideDownloadingComic

        private event EventHandler<OverrideDownloadingComicEventArgs> overrideDownloadingComic;

        public event EventHandler<OverrideDownloadingComicEventArgs> OverrideDownloadingComic
        {
            add { overrideDownloadingComic += value; }
            remove { overrideDownloadingComic -= value; }
        }

        protected virtual void OnOverrideDownloadingComic(OverrideDownloadingComicEventArgs e)
        {
            var reference = overrideDownloadingComic;
            if (reference != null)
                reference(this, e);
        }

        private OverrideDownloadingComicEventArgs OnOverrideDownloadingComic(Comic comic, bool comicHasFinishedDownloading)
        {
            var e = new OverrideDownloadingComicEventArgs(comic) { ComicHasFinishedDownloading = comicHasFinishedDownloading };
            OnOverrideDownloadingComic(e);

            return e;
        }

        #endregion
    }
}