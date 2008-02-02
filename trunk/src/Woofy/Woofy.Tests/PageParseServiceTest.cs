using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MbUnit.Framework;
using Rhino.Mocks;

using Woofy.Services;
using Woofy.Entities;
using System.Net;
using Woofy.Other;
using System.IO;

namespace Woofy.Tests
{
    [TestFixture]
    public class PageParseServiceTest
    {
        private const string RetrieveLinksFromPageByRegex_PageContent = @"
<a href=""dir1/bear.html"">Fuzzy Wuzzy was a bear</a>
<a href=""http://www.myurl.com/dir1/hair.html"">Fuzzy Wuzzy had no hair</a>
<a href=""/dir1/fuzzy.html"">So Fuzzy Wuzzy wasn't fuzzy</a>
<a href=""http://myurl.com/dir1/washe.html"">Was he?</a>";

        private MockRepository _mocks;
        private WebClientWrapper _webClient;
        private PageParseService _pageParseService;
        private PageParseService _pageParseServiceMocked;

        #region SetUp/TearDown
        [SetUp]
        public void SetUp()
        {
            _mocks = new MockRepository();

            _webClient = _mocks.PartialMock<WebClientWrapper>();

            _pageParseService = new PageParseService(_webClient);
            _pageParseServiceMocked = _mocks.PartialMock<PageParseService>(_webClient);
        }
        #endregion

        #region RetrieveLinksFromPageByRegex
        [Test]
        public void ShouldWorkWhenRegexMatchesAbsoluteLinks()
        {
            Uri[] links = _pageParseService.RetrieveLinksFromPageByRegex(@"http://[^\n]*html",
                                                                            RetrieveLinksFromPageByRegex_PageContent,
                                                                            new Uri("http://www.myurl.com"));

            Assert.AreEqual(2, links.Length);
            Assert.AreEqual("http://www.myurl.com/dir1/hair.html", links[0].AbsoluteUri);
            Assert.AreEqual("http://myurl.com/dir1/washe.html", links[1].AbsoluteUri);
        }

        [Test]
        public void ShouldWorkWhenRegexWithContentGroupMatchesAbsoluteLinks()
        {
            Uri[] links = _pageParseService.RetrieveLinksFromPageByRegex(@"(?<content>http://[^\n]*html)",
                                                                            RetrieveLinksFromPageByRegex_PageContent,
                                                                            new Uri("http://www.myurl.com"));

            Assert.AreEqual(2, links.Length);
            Assert.AreEqual("http://www.myurl.com/dir1/hair.html", links[0].AbsoluteUri);
            Assert.AreEqual("http://myurl.com/dir1/washe.html", links[1].AbsoluteUri);
        }

        [Test]
        public void ShouldWorkWhenRegexMatchesRelativeLinks()
        {
            Uri[] links = _pageParseService.RetrieveLinksFromPageByRegex(@"(dir1/bear.html)|(/dir1/fuzzy.html)",
                                                                                RetrieveLinksFromPageByRegex_PageContent,
                                                                                new Uri("http://www.myurl.com/"));

            Assert.AreEqual(2, links.Length);
            Assert.AreEqual("http://www.myurl.com/dir1/bear.html", links[0].AbsoluteUri);
            Assert.AreEqual("http://www.myurl.com/dir1/fuzzy.html", links[1].AbsoluteUri);
        }

        [Test]
        public void ShouldWorkWhenRegexWithContentGroupMatchesRelativeLinks()
        {
            Uri[] links = _pageParseService.RetrieveLinksFromPageByRegex(@"(?<content>(dir1/bear.html)|(/dir1/fuzzy.html))",
                                                                                RetrieveLinksFromPageByRegex_PageContent,
                                                                                new Uri("http://www.myurl.com/"));

            Assert.AreEqual(2, links.Length);
            Assert.AreEqual("http://www.myurl.com/dir1/bear.html", links[0].AbsoluteUri);
            Assert.AreEqual("http://www.myurl.com/dir1/fuzzy.html", links[1].AbsoluteUri);
        }

        [Test]
        public void ShouldWorkWhenRegexMatchesAbsoluteAndRelativeLinks()
        {
            Uri[] links = _pageParseService.RetrieveLinksFromPageByRegex(@"(http://[^\n]*html)|(/dir1/fuzzy.html)",
                                                                                RetrieveLinksFromPageByRegex_PageContent,
                                                                                new Uri("http://www.myurl.com/"));

            Assert.AreEqual(3, links.Length);
            Assert.AreEqual("http://www.myurl.com/dir1/hair.html", links[0].AbsoluteUri);
            Assert.AreEqual("http://www.myurl.com/dir1/fuzzy.html", links[1].AbsoluteUri);
            Assert.AreEqual("http://myurl.com/dir1/washe.html", links[2].AbsoluteUri);
        }

        [Test]
        public void ShouldWorkWhenRegexWithContentGroupMatchesAbsoluteAndRelativeLinks()
        {
            Uri[] links = _pageParseService.RetrieveLinksFromPageByRegex(@"(?<content>(http://[^\n]*html)|(/dir1/fuzzy.html))",
                                                                                RetrieveLinksFromPageByRegex_PageContent,
                                                                                new Uri("http://www.myurl.com/"));

            Assert.AreEqual(3, links.Length);
            Assert.AreEqual("http://www.myurl.com/dir1/hair.html", links[0].AbsoluteUri);
            Assert.AreEqual("http://www.myurl.com/dir1/fuzzy.html", links[1].AbsoluteUri);
            Assert.AreEqual("http://myurl.com/dir1/washe.html", links[2].AbsoluteUri);
        }
        #endregion

        #region RetrieveFaviconUrlFromPage
        [Test]
        public void RetrieveFaviconUrlFromPage_ShouldReturnTheDefaultFaviconIfFound()
        {
            using (_mocks.Record())
            {
                Expect
                    .Call(_webClient.DownloadString(null))
                    .IgnoreArguments()
                    .Return(null);

                Expect
                    .Call(_pageParseServiceMocked.RetrieveLinksFromPageByRegex(null, null, null))
                    .IgnoreArguments()
                    .Return(new Uri[0]);

                Expect
                    .Call(_webClient.OpenRead(null))
                    .IgnoreArguments()
                    .Return(Stream.Null);
            }

            using (_mocks.Playback())
            {
                Uri faviconAddress = _pageParseServiceMocked.RetrieveFaviconAddressFromPage(new Uri("http://www.mysite.com/a/b"));
                Assert.AreEqual("http://www.mysite.com/favicon.ico", faviconAddress.AbsoluteUri);
            }
        }

        [Test]
        public void RetrieveFaviconUrlFromPage_ShouldReturnTheFirstFaviconMatch()
        {
            using (_mocks.Record())
            {
                Expect
                    .Call(_webClient.DownloadString(null))
                    .IgnoreArguments()
                    .Return(null);

                Expect
                    .Call(_pageParseServiceMocked.RetrieveLinksFromPageByRegex(null, null, null))
                    .IgnoreArguments()
                    .Return(new Uri[] { new Uri("http://www.mysite.com/myfavicon.ico"), new Uri("http://www.yoursite.com/yourfavicon.ico") });

                Expect
                    .Call(_webClient.OpenRead(null))
                    .IgnoreArguments()
                    .Repeat.Never();
            }

            using (_mocks.Playback())
            {
                Uri faviconAddress = _pageParseServiceMocked.RetrieveFaviconAddressFromPage(new Uri("http://mysite.com"));
                Assert.AreEqual("http://www.mysite.com/myfavicon.ico", faviconAddress.AbsoluteUri);
            }
        }

        [Test]
        public void RetrieveFaviconUrlFromPage_ShouldReturnNullIfNoFaviconsAreFound()
        {
            using (_mocks.Record())
            {
                Expect
                    .Call(_webClient.DownloadString(null))
                    .IgnoreArguments()
                    .Return(null);

                Expect
                    .Call(_pageParseServiceMocked.RetrieveLinksFromPageByRegex(null, null, null))
                    .IgnoreArguments()
                    .Return(new Uri[0]);

                Expect
                    .Call(_webClient.OpenRead(null))
                    .IgnoreArguments()
                    .Return(null);
            }

            using (_mocks.Playback())
            {
                Uri address = _pageParseServiceMocked.RetrieveFaviconAddressFromPage(new Uri("http://mysite.com"));
                Assert.IsNull(address);
            }
        }

        [Test]
        public void RetrieveFaviconUrlFromPage_ShouldReturnNullOnWebException()
        {
            using (_mocks.Record())
            {
                Expect
                    .Call(_webClient.OpenRead(null))
                    .IgnoreArguments()
                    .Repeat.Never();

                Expect
                    .Call(_webClient.DownloadString(null))
                    .IgnoreArguments()
                    .Throw(new WebException());
            }

            using (_mocks.Playback())
            {
                Uri address = _pageParseServiceMocked.RetrieveFaviconAddressFromPage(new Uri("http://mysite.com"));
                Assert.IsNull(address);
            }
        }


        #endregion
    }
}
