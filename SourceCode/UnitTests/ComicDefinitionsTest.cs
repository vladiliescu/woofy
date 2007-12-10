using System;
using System.Collections.Generic;
using System.IO;

using MbUnit.Framework;

using Woofy.Core;
using Woofy.Settings;

namespace UnitTests
{
    [FixtureCategory("Long-running")]
    [TestFixture(TimeOut = 180)]
    public class ComicDefinitionsTest
    {  
        private static readonly string ComicsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Comics");
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

        //[CombinatorialTest]
        public void TestDownloadsXUniqueComics(
            [UsingFactories("ComicInfos")] string comicInfoFile
            )
        {
            comicInfoFile = Path.Combine(ApplicationSettings.ComicDefinitionsFolder, comicInfoFile);

            ComicDefinition comicInfo = new ComicDefinition(comicInfoFile);
            CountingFileDownloader comicsDownloaderStub = new CountingFileDownloader();
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
            comicInfoFile = Path.Combine(ApplicationSettings.ComicDefinitionsFolder, comicInfoFile);

            ComicDefinition comicInfo = new ComicDefinition(comicInfoFile);
            CountingFileDownloader comicsDownloaderStub = new CountingFileDownloader();
            ComicsProvider comicsProvider = new ComicsProvider(comicInfo, comicsDownloaderStub);

            comicsProvider.DownloadComics(ComicsProvider.AllAvailableComics);
            
            string[] comics = comicsDownloaderStub.ComicLinks;
            Assert.AreEqual(comics[comics.Length - 1], comicInfo.FirstIssue);
        }

        [Factory(typeof(string))]
        public IEnumerable<string> ComicInfos()
        {
            //int i = 0;
            foreach (string comicInfoFile in Directory.GetFiles(ApplicationSettings.ComicDefinitionsFolder, "*.xml"))
            {
                //i++;
                //if (i > 14)
                if (File.GetLastWriteTime(comicInfoFile) > new DateTime(2007, 11, 17)
                    && !comicInfoFile.Contains("Blastwave")
                    && !comicInfoFile.Contains("LeastICouldDo")
                    && !comicInfoFile.Contains("MegaTokyo")
                    && !comicInfoFile.Contains("Penny")
                    && !comicInfoFile.Contains("Order")
                    && !comicInfoFile.Contains("Lowroad")
                    && !comicInfoFile.Contains("Castlevania")
                    && !comicInfoFile.Contains("Cyanide")
                    && !comicInfoFile.Contains("PvPOnline")
                    && !comicInfoFile.Contains("RealLife")
                    && !comicInfoFile.Contains("Striptease")
                    && !comicInfoFile.Contains("Exterminatus")
                    && !comicInfoFile.Contains("Arlo")
                    && !comicInfoFile.Contains("GirlGenius")
                    && !comicInfoFile.Contains("TwoK")
                    && !comicInfoFile.Contains("Undert")
                    )
                    yield return Path.GetFileName(comicInfoFile);
            }
        }
    }
}
