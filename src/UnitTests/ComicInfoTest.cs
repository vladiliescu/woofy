using System;
using System.IO;
using System.Text;

using MbUnit.Framework;

using Woofy.Core;
using Woofy.Exceptions;

namespace UnitTests
{
    [TestFixture]
    public class ComicInfoTest
    {
        [Test]
        public void TestInitializesCorrectly()
        {
            var comicInfoContent = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<comicInfo friendlyName=""some friendly name"" 
            allowMultipleStrips=""true"" 
            allowMissingStrips=""false"" 
            author=""some author""
            authorEmail=""some author email"">
    <comicRegex><![CDATA[some comic regex]]></comicRegex>
    <rootUrl><![CDATA[some root url]]></rootUrl>
    <firstIssue><![CDATA[some first issue url]]></firstIssue>
    <latestPageRegex><![CDATA[some latest page regex]]></latestPageRegex>
    <startUrl><![CDATA[some base url]]></startUrl>
    <backButtonRegex><![CDATA[some back button regex]]></backButtonRegex>
    <captures> 
		<capture name=""capture1""><![CDATA[capture1 content]]></capture>
		<capture name=""capture2""><![CDATA[capture2 content]]></capture>
	</captures>
	<renamePattern><![CDATA[rename pattern]]></renamePattern>
</comicInfo>
";
            var definition = new ComicDefinition(new MemoryStream(Encoding.UTF8.GetBytes(comicInfoContent)));
            Assert.AreEqual("some friendly name", definition.FriendlyName);
            Assert.AreEqual(true, definition.AllowMultipleStrips);
            Assert.AreEqual(false, definition.AllowMissingStrips);
            Assert.AreEqual("some author", definition.Author);
            Assert.AreEqual("some author email", definition.AuthorEmail);
            Assert.AreEqual("some base url", definition.StartUrl);
            Assert.AreEqual("some root url", definition.RootUrl);
            Assert.AreEqual("some first issue url", definition.FirstIssue);
            Assert.AreEqual("some comic regex", definition.ComicRegex);
            Assert.AreEqual("some back button regex", definition.BackButtonRegex);
            Assert.AreEqual("some latest page regex", definition.LatestPageRegex);
            Assert.AreEqual(2, definition.Captures.Count);
            Assert.AreEqual("capture1", definition.Captures[0].Name);
            Assert.AreEqual("capture1 content", definition.Captures[0].Content);
            Assert.AreEqual("capture2", definition.Captures[1].Name);
            Assert.AreEqual("capture2 content", definition.Captures[1].Content);
            Assert.AreEqual("rename pattern", definition.RenamePattern);
        }

        [Test]
        public void TestWorksWithOnlyBareFields()
        {
            var comicInfoContent = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<comicInfo friendlyName=""some friendly name"" >
    <comicRegex><![CDATA[some comic regex]]></comicRegex>
    <startUrl><![CDATA[some base url]]></startUrl>
    <backButtonRegex><![CDATA[some back button regex]]></backButtonRegex>    
</comicInfo>
";
            var definition = new ComicDefinition(new MemoryStream(Encoding.UTF8.GetBytes(comicInfoContent)));
            Assert.AreEqual("some friendly name", definition.FriendlyName);
            Assert.AreEqual(false, definition.AllowMultipleStrips);
            Assert.AreEqual(false, definition.AllowMissingStrips);
            Assert.IsNull(definition.Author);
            Assert.IsNull(definition.AuthorEmail);
            Assert.AreEqual("some base url", definition.StartUrl);
            Assert.IsNull(definition.RootUrl);
            Assert.IsNull(definition.FirstIssue);
            Assert.AreEqual("some comic regex", definition.ComicRegex);
            Assert.AreEqual("some back button regex", definition.BackButtonRegex);
            Assert.IsNull(definition.LatestPageRegex);
            Assert.AreEqual(0, definition.Captures.Count);
            Assert.IsNull(definition.RenamePattern);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void TestThrowsExceptionOnMissingFriendlyName()
        {
            string comicInfoContent = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<comicInfo >
    <startUrl><![CDATA[some base url]]></startUrl>
    <comicRegex><![CDATA[some comic regex]]></comicRegex>
    <backButtonRegex><![CDATA[some back button regex]]></backButtonRegex>
</comicInfo>
";
            new ComicDefinition(new MemoryStream(Encoding.UTF8.GetBytes(comicInfoContent)));
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
            new ComicDefinition(new MemoryStream(Encoding.UTF8.GetBytes(comicInfoContent)));
        }
    }
}
