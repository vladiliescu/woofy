using System;
using System.IO;

using MbUnit.Framework;

using Woofy.Core;
using Woofy.Exceptions;


namespace UnitTests
{
    [TestFixture]
    public class TestComicInfo
    {
        [Test]
        public void TestInitializesCorrectly()
        {
            ComicInfo comicInfo = new ComicInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Files\TestComicInfo\basicComicInfo.xml"));
            Assert.AreEqual("some friendly name", comicInfo.FriendlyName);
            Assert.AreEqual("some base url", comicInfo.StartUrl);
            Assert.AreEqual("some first issue url", comicInfo.FirstIssue);
            Assert.AreEqual("some comic regex", comicInfo.ComicRegex);
            Assert.AreEqual("some back button regex", comicInfo.BackButtonRegex);
        }

        [Test]
        [ExpectedException(typeof(MissingFriendlyNameException))]
        public void TestThrowsExceptionOnMissingFriendlyName()
        {
            new ComicInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Files\TestComicInfo\missingFriendlyName.xml"));
        }

        [Test]
        [ExpectedException(typeof(MissingFriendlyNameException))]
        public void TestThrowsExceptionOnEmptyFriendlyName()
        {
            new ComicInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Files\TestComicInfo\emptyFriendlyName.xml"));
        }
    }
}
