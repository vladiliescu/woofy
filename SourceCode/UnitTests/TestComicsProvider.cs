using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using MbUnit.Framework;
using Woofy.Core;

namespace UnitTests
{
    [FixtureCategory("ComicInfo")]
    [TestFixture(TimeOut = 30)]
    public class TestComicsProvider
    {
        private static readonly string ComicsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Comics");
        private static readonly string ComicInfosDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ComicInfos");
        [SetUp]
        public void SetUpCreateComicsDirectory()
        {
            if (!Directory.Exists(ComicsDirectory))
                Directory.CreateDirectory(ComicsDirectory);
        }

        [TearDown]
        public void TearDownClearComicsDirectory()
        {
            if (Directory.Exists(ComicsDirectory))
                Directory.Delete(ComicsDirectory, true);
        }

        [CombinatorialTest]
        public void TestDownloadsXUniqueComics(
            [UsingFactories("ComicInfos")] string comicInfoFile
            )
        {
            comicInfoFile = Path.Combine(ComicInfosDirectory, comicInfoFile);

            ComicInfo comicInfo = new ComicInfo(comicInfoFile);
            ComicsDownloaderStub comicsDownloaderStub = new ComicsDownloaderStub();
            ComicsProvider comicsProvider = new ComicsProvider(comicInfo, comicsDownloaderStub);
            
            int comicsToDownload = 5;
            comicsProvider.DownloadComics(comicsToDownload);

            string[] comics = comicsDownloaderStub.ComicLinks;
            Assert.AreEqual(comicsToDownload, comics.Length, "number of comics to download");

            for (int i = 0; i < comics.Length; i++)
                for (int j = i + 1; j < comics.Length; j++)
                    Assert.AreNotEqual(comics[i], comics[j], "individual comics");
        }

        [CombinatorialTest]
        public void TestReachesFirstComic(
            [UsingFactories("ComicInfos")] string comicInfoFile
            )
        {
            comicInfoFile = Path.Combine(ComicInfosDirectory, comicInfoFile);

            ComicInfo comicInfo = new ComicInfo(comicInfoFile);
            ComicsDownloaderStub comicsDownloaderStub = new ComicsDownloaderStub();
            ComicsProvider comicsProvider = new ComicsProvider(comicInfo, comicsDownloaderStub);

            comicsProvider.DownloadComics(ComicsProvider.AllAvailableComics);
            
            string[] comics = comicsDownloaderStub.ComicLinks;
            Assert.AreEqual(comics[comics.Length - 1], comicInfo.FirstIssue);
        }

        [CombinatorialTest]
        public void TestReturnsOnlyOneComicLinkAndOneBackButtonLink(
            [UsingFactories("ComicInfos")] string comicInfoFile
            )
        {
            comicInfoFile = Path.Combine(ComicInfosDirectory, comicInfoFile);

            ComicInfo comicInfo = new ComicInfo(comicInfoFile);
            ComicsProvider comicsProvider = new ComicsProvider(comicInfo, ComicsDirectory);

            using (WebClient client = new WebClient())
            {
                client.Credentials = CredentialCache.DefaultNetworkCredentials;
                string currentUrl = comicInfo.StartUrl;
                //Iterate through two pages, to make sure we have a forward button too (and not only a back button, as it is common for the latest archive page).
                for (int i = 0; i < 2; i++)
                {
                    string pageContent = client.DownloadString(currentUrl);

                    string[] comicLinks = comicsProvider.RetrieveComicLinksFromPage(pageContent, comicInfo);
                    string[] backButtonLinks = comicsProvider.RetrieveBackButtonLinksFromPage(pageContent, comicInfo);

                    Assert.AreEqual(1, comicLinks.Length, "number of comic links");
                    Assert.AreEqual(1, backButtonLinks.Length, "number of back button links");
                    Assert.AreNotEqual(string.Empty, backButtonLinks[0], "back button link");
                    Assert.AreNotEqual(null, backButtonLinks[0], "back button link");

                    currentUrl = backButtonLinks[0];
                }
            }
        }

        [Factory(typeof(string))]
        public IEnumerable<string> ComicInfos()
        {
            foreach (string comicInfoFile in Directory.GetFiles(ComicInfosDirectory, "*.*"))
                yield return Path.GetFileName(comicInfoFile);
        }
    }
}
