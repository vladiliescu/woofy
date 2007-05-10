using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Net;

using MbUnit.Framework;

using Woofy.Core;

namespace UnitTests
{
    [FixtureCategory("ComicInfo")]
    [TestFixture]
    public class TestComicsDownloader
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

        [Test]
        public void TestDownloadsXComics()
        {
            string[] comics = new string[] {
                "http://www.pvponline.com/images/3018.gif",
                "http://www.pvponline.com/images/3017.gif"
            };

            
            ComicsDownloader downloadComicHandler = new ComicsDownloader();
            int downloadedComicsCount = downloadComicHandler.DownloadComics(comics, ComicsDirectory);

            string[] downloadedComics = Directory.GetFiles(ComicsDirectory);

            Array.Sort(comics);
            Assert.AreEqual(comics.Length, downloadedComics.Length, "downloaded files count");
            Assert.AreEqual(comics.Length, downloadedComicsCount, "reported downloaded files count");

            for (int i = 0; i < comics.Length; i++)
            {
                string comicName = comics[i].Substring(comics[i].LastIndexOf('/') + 1);
                string downloadedComicName = Path.GetFileName(downloadedComics[i]);
                Assert.AreEqual(comicName, downloadedComicName, "comic name");
            }
        }
        
        [CombinatorialTest]
        public void TestRecursiveDownloadStopsWhenExistingComicFound(
            [UsingFactories("ComicInfos")] string comicInfoFile
            )
        {
            ComicInfo comicInfo = new ComicInfo(comicInfoFile);
            ComicsProvider comicsProvider = new ComicsProvider(comicInfo);
            ComicsDownloader comicsHandler = new ComicsDownloader();

            using (WebClient client = new WebClient())
            {
                client.Credentials = CredentialCache.DefaultNetworkCredentials;
                
                string pageContent = client.DownloadString(comicInfo.StartUrl);
                string comicLink = comicsProvider.RetrieveComicLinkFromPage(pageContent, comicInfo);
                
                //Create a file with the same name in the comics directory, so that a new comic will not be downloaded.
                string existingFileName = Path.Combine(ComicsDirectory, comicLink.Substring(comicLink.LastIndexOf('/') + 1));
                File.AppendAllText(existingFileName, string.Empty);
                
                bool comicDownloaded = comicsHandler.DownloadComic(comicLink, ComicsDirectory);

                FileInfo existingFileInfo = new FileInfo(existingFileName);
                
                Assert.AreEqual(0, existingFileInfo.Length, "file length");
                Assert.AreEqual(comicDownloaded, false);
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
