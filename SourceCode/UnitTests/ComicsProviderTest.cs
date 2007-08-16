using System;
using System.Collections.Generic;
using System.Text;

using MbUnit.Framework;

using Woofy.Core;

namespace UnitTests
{
    [TestFixture]
    public class ComicsProviderTest
    {
        private const string PageContent = @"
<a href=""dir1/bear.html"">Fuzzy Wuzzy was a bear</a>
<a href=""http://www.myurl.com/dir1/hair.html"">Fuzzy Wuzzy had no hair</a>
<a href=""/dir1/fuzzy.html"">So Fuzzy Wuzzy wasn't fuzzy</a>
<a href=""http://myurl.com/dir1/washe.html"">Was he?</a>";

        #region RetrieveLinksFromPage
        [Test]
        public void TestWorksWithSimpleRegex()
        {
            string[] links = ComicsProvider.RetrieveLinksFromPage(PageContent,
                                                                "http://www.myurl.com",
                                                                @"http://[^\n]*html");

            Assert.AreEqual(2, links.Length);
            Assert.AreEqual("http://www.myurl.com/dir1/hair.html", links[0]);
            Assert.AreEqual("http://myurl.com/dir1/washe.html", links[1]);
        }

        [Test]
        public void TestCombinesRelativeCaptureWithCurrentUrl()
        {
            string[] links = ComicsProvider.RetrieveLinksFromPage(PageContent,
                                                                "http://www.myurl.com/mycomicsdir",
                                                                @"(?<content>(dir1/bear.html)|(/dir1/fuzzy.html))");

            Assert.AreEqual(2, links.Length);
            Assert.AreEqual("http://www.myurl.com/mycomicsdir/dir1/bear.html", links[0]);
            Assert.AreEqual("http://www.myurl.com/mycomicsdir/dir1/fuzzy.html", links[1]);
        }

        [Test]
        public void TestCombinesRelativeContentWithCurrentUrl()
        {
            string[] links = ComicsProvider.RetrieveLinksFromPage(PageContent,
                                                                "http://www.myurl.com/mycomicsdir",
                                                                @"(dir1/bear.html)|(/dir1/fuzzy.html)");

            Assert.AreEqual(2, links.Length);
            Assert.AreEqual("http://www.myurl.com/mycomicsdir/dir1/bear.html", links[0]);
            Assert.AreEqual("http://www.myurl.com/mycomicsdir/dir1/fuzzy.html", links[1]);
        }

        [Test]
        public void TestReturnsAbsoluteCapture()
        {
            string[] links = ComicsProvider.RetrieveLinksFromPage(PageContent,
                                                                "http://www.myurl.com/mycomicsdir",
                                                                @"<a\shref=""(?<content>http://[^\n]*html)");

            Assert.AreEqual(2, links.Length);
            Assert.AreEqual("http://www.myurl.com/dir1/hair.html", links[0]);
            Assert.AreEqual("http://myurl.com/dir1/washe.html", links[1]);
        }

        [Test]
        public void TestReturnsAbsoluteContent()
        {
            string[] links = ComicsProvider.RetrieveLinksFromPage(PageContent,
                                                                "http://www.myurl.com/mycomicsdir",
                                                                @"http://[^\n]*html");

            Assert.AreEqual(2, links.Length);
            Assert.AreEqual("http://www.myurl.com/dir1/hair.html", links[0]);
            Assert.AreEqual("http://myurl.com/dir1/washe.html", links[1]);
        } 
        #endregion

        #region MatchedLinksObeyRules

        [Test]
        public void TestMissingStripsAllowed()
        {
            DownloadOutcome downloadOutcome = DownloadOutcome.Successful;
            bool result = ComicsProvider.MatchedLinksObeyRules(new string[0], true, false, ref downloadOutcome);

            Assert.AreEqual(true, result);
            Assert.AreEqual(DownloadOutcome.Successful, downloadOutcome);
        }

        [Test]
        public void TestMissingStripsNotAllowed()
        {
            DownloadOutcome downloadOutcome = DownloadOutcome.Successful;
            bool result = ComicsProvider.MatchedLinksObeyRules(new string[0], false, false, ref downloadOutcome);

            Assert.AreEqual(false, result);
            Assert.AreEqual(DownloadOutcome.NoStripMatchesRuleBroken, downloadOutcome);
        }

        [Test]
        public void TestMultipleStripsAllowed()
        {
            DownloadOutcome downloadOutcome = DownloadOutcome.Successful;
            bool result = ComicsProvider.MatchedLinksObeyRules(new string[2], false, true, ref downloadOutcome);

            Assert.AreEqual(true, result);
            Assert.AreEqual(DownloadOutcome.Successful, downloadOutcome);
        }

        [Test]
        public void TestMultipleStripsNotAllowed()
        {
            DownloadOutcome downloadOutcome = DownloadOutcome.Successful;
            bool result = ComicsProvider.MatchedLinksObeyRules(new string[2], false, false, ref downloadOutcome);

            Assert.AreEqual(false, result);
            Assert.AreEqual(DownloadOutcome.MultipleStripMatchesRuleBroken, downloadOutcome);
        }

        [Test]
        public void TestOnlyOneStrip()
        {
            DownloadOutcome downloadOutcome = DownloadOutcome.Successful;
            bool result = ComicsProvider.MatchedLinksObeyRules(new string[1], false, false, ref downloadOutcome);

            Assert.AreEqual(true, result);
            Assert.AreEqual(DownloadOutcome.Successful, downloadOutcome);
        }

        #endregion

        #region GetProperStartUrlFromPage
        [Test]
        public void TestReturnsProperStartUrlWithoutLatestPageRegex()
        {
            string startUrl = 
                ComicsProvider.GetProperStartUrlFromPage(PageContent, "http://www.myurl.com/mycomicsdir", null);

            Assert.AreEqual("http://www.myurl.com/mycomicsdir", startUrl);
        }

        [Test]
        public void TestReturnsProperStartUrlWithLatestPageRegex()
        {
            string startUrl =
                ComicsProvider.GetProperStartUrlFromPage(PageContent, "http://www.myurl.com/mycomicsdir", @"http://[^\n]*hair.html");

            Assert.AreEqual("http://www.myurl.com/mycomicsdir", startUrl);
        }

        [Test]
        public void TestReturnsProperStartUrlWithoutAnyRegexMatches()
        {
            string startUrl =
                ComicsProvider.GetProperStartUrlFromPage(PageContent, "http://www.myurl.com/mycomicsdir", @"http://[^\n]*html");

            Assert.AreEqual("http://www.myurl.com/dir1/hair.html", startUrl);
        }

        [Test]
        public void TestReturnsProperStartUrlWithMultipleRegexMatches()
        {
            Assert.Fail();
        } 
        #endregion
    }
}
