using System;
using System.IO;
using System.Text;


using Woofy.Core;
using Woofy.Exceptions;
using Xunit;

namespace UnitTests
{
    public class ComicInfoTest
    {
        [Fact]
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
            Assert.Equal("some friendly name", definition.FriendlyName);
            Assert.Equal(true, definition.AllowMultipleStrips);
            Assert.Equal(false, definition.AllowMissingStrips);
            Assert.Equal("some author", definition.Author);
            Assert.Equal("some author email", definition.AuthorEmail);
            Assert.Equal("some base url", definition.StartUrl);
            Assert.Equal("some root url", definition.RootUrl);
            Assert.Equal("some first issue url", definition.FirstIssue);
            Assert.Equal("some comic regex", definition.ComicRegex);
            Assert.Equal("some back button regex", definition.BackButtonRegex);
            Assert.Equal("some latest page regex", definition.LatestPageRegex);
            Assert.Equal(2, definition.Captures.Count);
            Assert.Equal("capture1", definition.Captures[0].Name);
            Assert.Equal("capture1 content", definition.Captures[0].Content);
            Assert.Equal("capture2", definition.Captures[1].Name);
            Assert.Equal("capture2 content", definition.Captures[1].Content);
            Assert.Equal("rename pattern", definition.RenamePattern);
        }

        [Fact]
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
            Assert.Equal("some friendly name", definition.FriendlyName);
            Assert.Equal(false, definition.AllowMultipleStrips);
            Assert.Equal(false, definition.AllowMissingStrips);
            Assert.Null(definition.Author);
            Assert.Null(definition.AuthorEmail);
            Assert.Equal("some base url", definition.StartUrl);
            Assert.Null(definition.RootUrl);
            Assert.Null(definition.FirstIssue);
            Assert.Equal("some comic regex", definition.ComicRegex);
            Assert.Equal("some back button regex", definition.BackButtonRegex);
            Assert.Null(definition.LatestPageRegex);
            Assert.Equal(0, definition.Captures.Count);
            Assert.Null(definition.RenamePattern);
        }

        [Fact]
        public void TestThrowsExceptionOnMissingFriendlyName()
        {
            string comicInfoContent = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<comicInfo >
    <startUrl><![CDATA[some base url]]></startUrl>
    <comicRegex><![CDATA[some comic regex]]></comicRegex>
    <backButtonRegex><![CDATA[some back button regex]]></backButtonRegex>
</comicInfo>
";
			Assert.Throws<Exception>(() => new ComicDefinition(new MemoryStream(Encoding.UTF8.GetBytes(comicInfoContent))));
        }

        [Fact]
        public void TestThrowsExceptionOnEmptyFriendlyName()
        {
            string comicInfoContent = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<comicInfo  friendlyName="""" >
    <startUrl><![CDATA[some base url]]></startUrl>
    <comicRegex><![CDATA[some comic regex]]></comicRegex>
    <backButtonRegex><![CDATA[some back button regex]]></backButtonRegex>
</comicInfo>
";
            Assert.Throws<MissingFriendlyNameException>(() => new ComicDefinition(new MemoryStream(Encoding.UTF8.GetBytes(comicInfoContent))));
        }
    }
}
