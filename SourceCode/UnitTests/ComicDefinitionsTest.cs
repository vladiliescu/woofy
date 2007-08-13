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
    public class ComicDefinitionsTest
    {  
        private static readonly string ComicsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Comics");
        private static readonly string ComicInfosDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ComicInfos");
        private const int ComicsToDownload = 5;

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
            
            comicsProvider.DownloadComics(ComicsToDownload);

            string[] comics = comicsDownloaderStub.ComicLinks;
            Assert.AreEqual(ComicsToDownload, comics.Length, "number of comics to download");

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

        [Factory(typeof(string))]
        public IEnumerable<string> ComicInfos()
        {
            foreach (string comicInfoFile in Directory.GetFiles(ComicInfosDirectory, "*.*"))
                yield return Path.GetFileName(comicInfoFile);
        }
    }
}
