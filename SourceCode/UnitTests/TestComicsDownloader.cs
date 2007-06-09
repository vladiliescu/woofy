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
        
        [CombinatorialTest]
        public void TestRecursiveDownloadStopsWhenExistingComicFound(
            [UsingFactories("ComicInfos")] string comicInfoFile
            )
        {
            ComicInfo comicInfo = new ComicInfo(comicInfoFile);
            ComicsProvider comicsProvider = new ComicsProvider(comicInfo, ComicsDirectory);
            ComicsDownloader comicsHandler = new ComicsDownloader(ComicsDirectory);

            using (WebClient client = new WebClient())
            {
                client.Credentials = CredentialCache.DefaultNetworkCredentials;
                
                string pageContent = client.DownloadString(comicInfo.StartUrl);
                string comicLink = comicsProvider.RetrieveComicLinkFromPage(pageContent, comicInfo);
                
                //Create a file with the same name in the comics directory, so that a new comic will not be downloaded.
                string existingFileName = Path.Combine(ComicsDirectory, comicLink.Substring(comicLink.LastIndexOf('/') + 1));
                File.AppendAllText(existingFileName, string.Empty);
                
                bool comicAlreadyDownloaded;
                comicsHandler.DownloadComic(comicLink, out comicAlreadyDownloaded);

                FileInfo existingFileInfo = new FileInfo(existingFileName);
                
                Assert.AreEqual(0, existingFileInfo.Length, "file length");
                Assert.AreEqual(comicAlreadyDownloaded, true);
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
