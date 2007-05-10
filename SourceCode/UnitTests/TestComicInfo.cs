using System;
using System.Collections.Generic;
using System.Text;

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
            ComicInfo comicInfo = new ComicInfo(@"Files\TestComicInfo\basicComicInfo.xml");
            Assert.AreEqual("some friendly name", comicInfo.FriendlyName);
            Assert.AreEqual("some base url", comicInfo.StartUrl);
            Assert.AreEqual("some comic regex", comicInfo.ComicRegex);
            Assert.AreEqual("some back button regex", comicInfo.BackButtonRegex);
        }

        [Test]
        [ExpectedException(typeof(MissingFriendlyNameException))]
        public void TestThrowsExceptionOnMissingFriendlyName()
        {
            ComicInfo comicInfo = new ComicInfo(@"Files\TestComicInfo\missingFriendlyName.xml");
        }

        [Test]
        [ExpectedException(typeof(MissingFriendlyNameException))]
        public void TestThrowsExceptionOnEmptyFriendlyName()
        {
            ComicInfo comicInfo = new ComicInfo(@"Files\TestComicInfo\emptyFriendlyName.xml");
        }
    }
}
