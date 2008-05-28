using System;
using System.Collections.Generic;
using System.Text;
using Woofy.Entities;
using Woofy.Services;
using Woofy.Other;
using System.IO;
using System.Net;
using Woofy.EventArguments;

namespace Woofy.Controllers
{
    public class ComicAdapter
    {
        private FileDownloadService _fileDownloadService = new FileDownloadService();
        private PageParseService _pageParseService = new PageParseService();
        private FileWrapper _file = new FileWrapper();
        private PathWrapper _path = new PathWrapper();
        private WebClientWrapper _webClient = new WebClientWrapper();
        private bool _isDownloadAborted = false;

        public void AbortDownload()
        {
            _isDownloadAborted = true;
        }

        private Uri GetStartAddress(ComicDefinition definition, ComicStrip mostRecentStrip)
        {
            if (mostRecentStrip == null)
            {
                return _pageParseService.GetLatestPageOrStartAddress(definition.HomePageAddress, definition.LatestIssueRegex);
            }
            else
            {
                string pageContent = _webClient.DownloadString(mostRecentStrip.SourcePageAddress);
                Uri[] nextStripLinks = _pageParseService.RetrieveLinksFromPageByRegex(definition.NextIssueRegex, pageContent, mostRecentStrip.SourcePageAddress);
                if (nextStripLinks.Length == 0)
                    return null;

                return nextStripLinks[0];
            }
        }

        public void CheckComicForUpdates(Comic comic, ComicStrip mostRecentStrip)
        {
            ComicDefinition definition = comic.Definition;
            string downloadFolder = _path.Combine(ApplicationSettings.DefaultDownloadFolder, comic.Name);
            if (!Directory.Exists(downloadFolder))
                Directory.CreateDirectory(downloadFolder);

            try
            {
                Uri startAddress = GetStartAddress(definition, mostRecentStrip);
                Uri currentAddress = startAddress;
                Uri nextAddress;

                do
                {
                    //if (_isDownloadCancelled)
                    //{
                    //    downloadOutcome = DownloadOutcome.Cancelled;
                    //    break;
                    //}

                    DownloadStrip(currentAddress, definition, downloadFolder, out nextAddress);
                    OnDownloadedAllStripsFromPage(new DownloadedAllStripsFromPageEventArgs());

                    currentAddress = nextAddress;
                }
                while (nextAddress != null);
            }
            catch (UriFormatException ex)
            {
            }
            catch (WebException ex)
            {
                //TODO: trebuie sa raportez exceptiile ca erori.
            }
        }

        private void DownloadStrip(Uri address, ComicDefinition definition, string downloadFolder, out Uri nextAddress)
        {
            string pageContent = _webClient.DownloadString(address);

            Uri[] comicLinks = _pageParseService.RetrieveLinksFromPageByRegex(definition.StripRegex, pageContent, address);
            Uri[] nextStripLinks = _pageParseService.RetrieveLinksFromPageByRegex(definition.NextIssueRegex, pageContent, address);

            if (!MatchedLinksObeyRules(comicLinks.Length, definition.AllowMissingStrips, definition.AllowMultipleStrips))//, ref downloadOutcome))
            {
                nextAddress = null;
                return;
            }

            foreach (Uri comicLink in comicLinks)
            {
                string stripFileName = comicLink.AbsoluteUri.Substring(comicLink.AbsoluteUri.LastIndexOf('/') + 1);
                string downloadPath = _path.Combine(downloadFolder, stripFileName);

                ComicStrip strip = new ComicStrip(definition.Comic);
                strip.SourcePageAddress = address;
                strip.FilePath = downloadPath;

                DownloadingStripEventArgs e = OnDownloadingStrip(strip);
                if (e.AbortDownload)
                {
                    nextAddress = null;
                    return;
                }

                if (!e.SkipStrip)
                    _fileDownloadService.DownloadFile(comicLink, downloadPath, address);

                OnDownloadedStrip(strip);
            }

            if (nextStripLinks.Length == 0)
            {
                nextAddress = null;
                return;
            }
            
            nextAddress = nextStripLinks[0];
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

            string faviconName = comic.Id.ToString() + ".ico";
            string faviconPath = _path.Combine(ApplicationSettings.FaviconsFolder, faviconName);

            _file.Delete(faviconPath);
            _file.Move(faviconTempPath, faviconPath);

            comic.FaviconPath = faviconPath;
        }


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

        private void OnDownloadedStrip(ComicStrip strip)
        {
            DownloadedStripEventArgs e = new DownloadedStripEventArgs(strip);
            OnDownloadedStrip(e);
        }

        private event EventHandler<DownloadedAllStripsFromPageEventArgs> _downloadedAllStripsFromPage;
        public event EventHandler<DownloadedAllStripsFromPageEventArgs> DownloadedAllStripsFromPage
        {
            add { _downloadedAllStripsFromPage += value; }
            remove { _downloadedAllStripsFromPage -= value; }
        }

        protected virtual void OnDownloadedAllStripsFromPage(DownloadedAllStripsFromPageEventArgs e)
        {
            EventHandler<DownloadedAllStripsFromPageEventArgs> reference = _downloadedAllStripsFromPage;
            if (reference != null)
                reference(this, e);
        }
    }
}
