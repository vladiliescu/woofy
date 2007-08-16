using System;
using System.IO;

using MbUnit.Framework;

using Woofy.Core;
using Woofy.Exceptions;
using System.Text;


namespace UnitTests
{
    [TestFixture]
    public class ComicInfoTest
    {
        [Test]
        public void TestInitializesCorrectly()
        {
            string comicInfoContent = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<comicInfo friendlyName=""some friendly name"" 
            allowMultipleStrips=""true"" 
            allowMissingStrips=""false"" 
            author=""some author""
            authorEmail=""some author email"">
    <comicRegex><![CDATA[some comic regex]]></comicRegex>
    <firstIssue><![CDATA[some first issue url]]></firstIssue>
    <startUrl><![CDATA[some base url]]></startUrl>
    <backButtonRegex><![CDATA[some back button regex]]></backButtonRegex>
    <latestPageRegex><![CDATA[some latest page regex]]></latestPageRegex>
</comicInfo>
";
            ComicInfo comicInfo = new ComicInfo(new MemoryStream(Encoding.UTF8.GetBytes(comicInfoContent)));
            Assert.AreEqual("some friendly name", comicInfo.FriendlyName);
            Assert.AreEqual(true, comicInfo.AllowMultipleStrips);
            Assert.AreEqual(false, comicInfo.AllowMissingStrips);
            Assert.AreEqual("some author", comicInfo.Author);
            Assert.AreEqual("some author email", comicInfo.AuthorEmail);
            Assert.AreEqual("some base url", comicInfo.StartUrl);
            Assert.AreEqual("some first issue url", comicInfo.FirstIssue);
            Assert.AreEqual("some comic regex", comicInfo.ComicRegex);
            Assert.AreEqual("some back button regex", comicInfo.BackButtonRegex);
            Assert.AreEqual("some latest page regex", comicInfo.LatestPageRegex);
        }

        [Test]
        [ExpectedException(typeof(MissingFriendlyNameException))]
        public void TestThrowsExceptionOnMissingFriendlyName()
        {
            string comicInfoContent = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<comicInfo >
    <startUrl><![CDATA[some base url]]></startUrl>
    <comicRegex><![CDATA[some comic regex]]></comicRegex>
    <backButtonRegex><![CDATA[some back button regex]]></backButtonRegex>
</comicInfo>
";
            new ComicInfo(new MemoryStream(Encoding.UTF8.GetBytes(comicInfoContent)));
        }

        [Test]
        [ExpectedException(typeof(MissingFriendlyNameException))]
        public void TestThrowsExceptionOnEmptyFriendlyName()
        {
            string comicInfoContent = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<comicInfo  friendlyName="""" >
    <startUrl><![CDATA[some base url]]></startUrl>
    <comicRegex><![CDATA[some comic regex]]></comicRegex>
    <backButtonRegex><![CDATA[some back button regex]]></backButtonRegex>
</comicInfo>
";
            new ComicInfo(new MemoryStream(Encoding.UTF8.GetBytes(comicInfoContent)));
        }
    }
}
