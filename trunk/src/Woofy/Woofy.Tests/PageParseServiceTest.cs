using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MbUnit.Framework;
using Rhino.Mocks;

using Woofy.Services;
using Woofy.Entities;
using System.Net;

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

        private const string GetFaviconUrl_PageContent = @"
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta http-equiv=""content-type"" content=""text/html; charset=utf-8"" />
    <meta name=""description"" content="""" />
    <link href=""default.css"" rel=""stylesheet"" type=""text/css"" />
    <link rel=""shortcut icon"" href=""myfavicon.ico"" />
    <link rel=""icon"" href=""favicon.ico"" />
</head>
";

        private MockRepository _mocks;
        private PageParseService _pageParserService;
        private PageParseService _pageParserServiceMocked;

        #region SetUp/TearDown
        [SetUp]
        public void SetUp()
        {
            _mocks = new MockRepository();

            _pageParserService = new PageParseService();            
            _pageParserServiceMocked = _mocks.PartialMock<PageParseService>();
        } 
        #endregion

        #region RetrieveLinksFromPageByRegex
        [Test]
        public void ShouldWorkWhenRegexMatchesAbsoluteLinks()
        {
            Uri[] links = _pageParserService.RetrieveLinksFromPageByRegex(@"http://[^\n]*html",
                                                                            RetrieveLinksFromPageByRegex_PageContent,
                                                                            new Uri("http://www.myurl.com"));

            Assert.AreEqual(2, links.Length);
            Assert.AreEqual("http://www.myurl.com/dir1/hair.html", links[0].AbsoluteUri);
            Assert.AreEqual("http://myurl.com/dir1/washe.html", links[1].AbsoluteUri);
        }

        [Test]
        public void ShouldWorkWhenRegexWithContentGroupMatchesAbsoluteLinks()
        {
            Uri[] links = _pageParserService.RetrieveLinksFromPageByRegex(@"(?<content>http://[^\n]*html)",
                                                                            RetrieveLinksFromPageByRegex_PageContent,
                                                                            new Uri("http://www.myurl.com"));

            Assert.AreEqual(2, links.Length);
            Assert.AreEqual("http://www.myurl.com/dir1/hair.html", links[0].AbsoluteUri);
            Assert.AreEqual("http://myurl.com/dir1/washe.html", links[1].AbsoluteUri);
        }

        [Test]
        public void ShouldWorkWhenRegexMatchesRelativeLinks()
        {
            Uri[] links = _pageParserService.RetrieveLinksFromPageByRegex(@"(dir1/bear.html)|(/dir1/fuzzy.html)",
                                                                                RetrieveLinksFromPageByRegex_PageContent,
                                                                                new Uri("http://www.myurl.com/"));

            Assert.AreEqual(2, links.Length);
            Assert.AreEqual("http://www.myurl.com/dir1/bear.html", links[0].AbsoluteUri);
            Assert.AreEqual("http://www.myurl.com/dir1/fuzzy.html", links[1].AbsoluteUri);
        }

        [Test]
        public void ShouldWorkWhenRegexWithContentGroupMatchesRelativeLinks()
        {
            Uri[] links = _pageParserService.RetrieveLinksFromPageByRegex(@"(?<content>(dir1/bear.html)|(/dir1/fuzzy.html))",
                                                                                RetrieveLinksFromPageByRegex_PageContent,
                                                                                new Uri("http://www.myurl.com/"));

            Assert.AreEqual(2, links.Length);
            Assert.AreEqual("http://www.myurl.com/dir1/bear.html", links[0].AbsoluteUri);
            Assert.AreEqual("http://www.myurl.com/dir1/fuzzy.html", links[1].AbsoluteUri);
        }

        [Test]
        public void ShouldWorkWhenRegexMatchesAbsoluteAndRelativeLinks()
        {
            Uri[] links = _pageParserService.RetrieveLinksFromPageByRegex(@"(http://[^\n]*html)|(/dir1/fuzzy.html)",
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
            Uri[] links = _pageParserService.RetrieveLinksFromPageByRegex(@"(?<content>(http://[^\n]*html)|(/dir1/fuzzy.html))",
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
        public void RetrieveFaviconUrlFromPage_ShouldReturnTheFirstFaviconMatch()
        {            
            Uri dummyUri = new Uri("http://mysite.com");

            using (_mocks.Record())
            {                
                Expect.Call(_pageParserServiceMocked.ReadPageContent(dummyUri)).Return(GetFaviconUrl_PageContent);
            }

            using (_mocks.Playback())
            {
                Uri actualUri = _pageParserServiceMocked.RetrieveFaviconUrlFromPage(dummyUri);
                Assert.AreEqual("http://mysite.com/myfavicon.ico", actualUri.AbsoluteUri);
            }
        }

        [Test]
        public void RetrieveFaviconUrlFromPage_ShouldReturnNullIfNoFaviconsAreFound()
        {
            Uri dummyUri = new Uri("http://mysite.com");

            using (_mocks.Record())
            {
                Expect
                    .Call(_pageParserServiceMocked.ReadPageContent(null))
                    .IgnoreArguments()
                    .Return("");
            }

            using (_mocks.Playback())
            {
                Uri actualUri = _pageParserServiceMocked.RetrieveFaviconUrlFromPage(dummyUri);
                Assert.IsNull(actualUri);
            }
        }

        [Test]
        public void RetrieveFaviconUrlFromPage_ShouldReturnNullOnWebException()
        {
            using (_mocks.Record())
            {
                Expect
                    .Call(_pageParserServiceMocked.ReadPageContent(null))
                    .IgnoreArguments()
                    .Throw(new WebException());                
            }

            using (_mocks.Playback())
            {
                Uri actualUri = _pageParserServiceMocked.RetrieveFaviconUrlFromPage(new Uri("http://mysite.com"));
                Assert.IsNull(actualUri);
            }
        }
        #endregion
    }
}
