using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

using MbUnit.Framework;

using Woofy.Core;

namespace UnitTests
{
    [FixtureCategory("ComicInfo")]
    [TestFixture]
    public class TestComicsProvider
    {
        private const string ComicsDirectory = "Comics";
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
            ComicInfo comicInfo = new ComicInfo(comicInfoFile);
            ComicsDownloaderStub comicsDownloaderStub = new ComicsDownloaderStub();
            ComicsProvider comicsProvider = new ComicsProvider(comicInfo, comicsDownloaderStub);
            
            int comicsToDownload = 3;
            comicsProvider.DownloadComicsRecursive(comicsToDownload, ComicsDirectory);

            string[] comics = comicsDownloaderStub.ComicLinks;
            Assert.AreEqual(comicsToDownload, comics.Length, "number of comics to download");

            for (int i = 0; i < comics.Length; i++)
                for (int j = i + 1; j < comics.Length; j++)
                    Assert.AreNotEqual(comics[i], comics[j], "individual comics");
        }
                
        [CombinatorialTest]
        public void TestReturnsOnlyOneComicLinkAndOneBackButtonLink(
            [UsingFactories("ComicInfos")] string comicInfoFile
            )
        {
            ComicInfo comicInfo = new ComicInfo(comicInfoFile);
            ComicsProvider comicsProvider = new ComicsProvider(comicInfo);

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
            foreach (string comicInfoFile in Directory.GetFiles("ComicInfos", "*.*"))
                yield return comicInfoFile;
        }        
    }
}
