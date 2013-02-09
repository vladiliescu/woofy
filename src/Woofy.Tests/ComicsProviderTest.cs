//using System;

//using Woofy.Core;
//using Xunit;

//namespace UnitTests
//{
//    public class ComicsProviderTest
//    {
//        private const string PageContent = @"
//<a href=""dir1/bear.html"">Fuzzy Wuzzy was a bear</a>
//<a href=""http://www.myurl.com/dir1/hair.html"">Fuzzy Wuzzy had no hair</a>
//<a href=""/dir1/fuzzy.html"">So Fuzzy Wuzzy wasn't fuzzy</a>
//<a href=""http://myurl.com/dir1/washe.html"">Was he?</a>";

//        #region RetrieveLinksFromPage
//        [Fact]
//        public void TestWorksWithSimpleRegex()
//        {
//            Uri[] links = ComicsProvider.RetrieveLinksFromPage(PageContent,
//                                                                new Uri("http://www.myurl.com"),
//                                                                @"http://[^\n]*html");

//            Assert.Equal(2, links.Length);
//            Assert.Equal("http://www.myurl.com/dir1/hair.html", links[0].AbsoluteUri);
//            Assert.Equal("http://myurl.com/dir1/washe.html", links[1].AbsoluteUri);
//        }

//        [Fact]
//        public void TestCombinesRelativeCaptureWithCurrentUrl()
//        {
//            Uri[] links = ComicsProvider.RetrieveLinksFromPage(PageContent,
//                                                                new Uri("http://www.myurl.com/mycomicsdir/"),
//                                                                @"(?<content>(dir1/bear.html)|(/dir1/fuzzy.html))");

//            Assert.Equal(2, links.Length);
//            Assert.Equal("http://www.myurl.com/mycomicsdir/dir1/bear.html", links[0].AbsoluteUri);
//            Assert.Equal("http://www.myurl.com/mycomicsdir/dir1/fuzzy.html", links[1].AbsoluteUri);
//        }

//        [Fact]
//        public void TestCombinesRelativeContentWithCurrentUrl()
//        {
//            Uri[] links = ComicsProvider.RetrieveLinksFromPage(PageContent,
//                                                                new Uri("http://www.myurl.com/mycomicsdir"),
//                                                                @"(dir1/bear.html)|(/dir1/fuzzy.html)");

//            Assert.Equal(2, links.Length);
//            Assert.Equal("http://www.myurl.com/mycomicsdir/dir1/bear.html", links[0].AbsoluteUri);
//            Assert.Equal("http://www.myurl.com/mycomicsdir/dir1/fuzzy.html", links[1].AbsoluteUri);
//        }

//        [Fact]
//        public void TestReturnsAbsoluteCapture()
//        {
//            Uri[] links = ComicsProvider.RetrieveLinksFromPage(PageContent,
//                                                                new Uri("http://www.myurl.com/mycomicsdir"),
//                                                                @"<a\shref=""(?<content>http://[^\n]*html)");

//            Assert.Equal(2, links.Length);
//            Assert.Equal("http://www.myurl.com/dir1/hair.html", links[0].AbsoluteUri);
//            Assert.Equal("http://myurl.com/dir1/washe.html", links[1].AbsoluteUri);
//        }

//        [Fact]
//        public void TestReturnsAbsoluteContent()
//        {
//            Uri[] links = ComicsProvider.RetrieveLinksFromPage(PageContent,
//                                                                new Uri("http://www.myurl.com/mycomicsdir"),
//                                                                @"http://[^\n]*html");

//            Assert.Equal(2, links.Length);
//            Assert.Equal("http://www.myurl.com/dir1/hair.html", links[0].AbsoluteUri);
//            Assert.Equal("http://myurl.com/dir1/washe.html", links[1].AbsoluteUri);
//        } 
//        #endregion

//        #region MatchedLinksObeyRules

//        [Fact]
//        public void TestMissingStripsAllowed()
//        {
//            DownloadOutcome downloadOutcome = DownloadOutcome.Successful;
//            bool result = ComicsProvider.MatchedLinksObeyRules(0, true, false, ref downloadOutcome);

//            Assert.Equal(true, result);
//            Assert.Equal(DownloadOutcome.Successful, downloadOutcome);
//        }

//        [Fact]
//        public void TestMissingStripsNotAllowed()
//        {
//            DownloadOutcome downloadOutcome = DownloadOutcome.Successful;
//            bool result = ComicsProvider.MatchedLinksObeyRules(0, false, false, ref downloadOutcome);

//            Assert.Equal(false, result);
//            Assert.Equal(DownloadOutcome.NoStripMatchesRuleBroken, downloadOutcome);
//        }

//        [Fact]
//        public void TestMultipleStripsAllowed()
//        {
//            DownloadOutcome downloadOutcome = DownloadOutcome.Successful;
//            bool result = ComicsProvider.MatchedLinksObeyRules(2, false, true, ref downloadOutcome);

//            Assert.Equal(true, result);
//            Assert.Equal(DownloadOutcome.Successful, downloadOutcome);
//        }

//        [Fact]
//        public void TestMultipleStripsNotAllowed()
//        {
//            DownloadOutcome downloadOutcome = DownloadOutcome.Successful;
//            bool result = ComicsProvider.MatchedLinksObeyRules(2, false, false, ref downloadOutcome);

//            Assert.Equal(false, result);
//            Assert.Equal(DownloadOutcome.MultipleStripMatchesRuleBroken, downloadOutcome);
//        }

//        [Fact]
//        public void TestOnlyOneStrip()
//        {
//            DownloadOutcome downloadOutcome = DownloadOutcome.Successful;
//            bool result = ComicsProvider.MatchedLinksObeyRules(1, false, false, ref downloadOutcome);

//            Assert.Equal(true, result);
//            Assert.Equal(DownloadOutcome.Successful, downloadOutcome);
//        }

//        #endregion

//        #region GetProperStartUrlFromPage
//        [Fact]
//        public void TestReturnsProperStartUrlWithoutLatestPageRegex()
//        {
//            string startUrl = 
//                ComicsProvider.GetProperStartUrlFromPage(PageContent, new Uri("http://www.myurl.com/mycomicsdir"), null);

//            Assert.Equal("http://www.myurl.com/mycomicsdir", startUrl);
//        }

//        [Fact]
//        public void TestReturnsProperStartUrlWithLatestPageRegex()
//        {
//            string startUrl =
//                ComicsProvider.GetProperStartUrlFromPage(PageContent, new Uri("http://www.myurl.com/mycomicsdir"), @"http://[^\n]*hair.html");

//            Assert.Equal("http://www.myurl.com/dir1/hair.html", startUrl);
//        }

//        [Fact]
//        public void TestReturnsProperStartUrlWithoutAnyRegexMatches()
//        {
//            string startUrl =
//                ComicsProvider.GetProperStartUrlFromPage(PageContent, new Uri("http://www.myurl.com/mycomicsdir"), @"jungle boogie");

//            Assert.Equal("http://www.myurl.com/mycomicsdir", startUrl);
//        }

//        [Fact]
//        public void TestReturnsProperStartUrlWithMultipleRegexMatches()
//        {
//            string startUrl =
//                ComicsProvider.GetProperStartUrlFromPage(PageContent, new Uri("http://www.myurl.com/mycomicsdir"), @"http://[^\n]*html");

//            Assert.Equal("http://www.myurl.com/dir1/hair.html", startUrl);
//        } 
//        #endregion
//    }
//}
