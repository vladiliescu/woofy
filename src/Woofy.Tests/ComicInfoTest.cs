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
<comicDefinition 
			name=""some comic name"" 
            allowMultipleStrips=""true"" 
            allowMissingStrips=""false"" 
            definitionAuthor=""some author"">
	<homePage><![CDATA[some home page]]></homePage>
	<startPage><![CDATA[some start page]]></startPage>    
	<comicRegex><![CDATA[some comic regex]]></comicRegex>
    <nextPageRegex><![CDATA[some next page regex]]></nextPageRegex>
	<rootUrl><![CDATA[some root url]]></rootUrl>
    <captures> 
		<capture name=""capture1""><![CDATA[capture1 content]]></capture>
		<capture name=""capture2""><![CDATA[capture2 content]]></capture>
	</captures>
	<renamePattern><![CDATA[rename pattern]]></renamePattern>
</comicDefinition>
";
			var definition = new ComicDefinition(new MemoryStream(Encoding.UTF8.GetBytes(comicInfoContent)));
            Assert.Equal("some comic name", definition.Name);
            Assert.Equal(true, definition.AllowMultipleStrips);
            Assert.Equal(false, definition.AllowMissingStrips);
            Assert.Equal("some author", definition.Author);
            Assert.Equal("some home page", definition.HomePage);
			Assert.Equal("some start page", definition.StartPage);
            Assert.Equal("some root url", definition.RootUrl);
            Assert.Equal("some comic regex", definition.ComicRegex);
			Assert.Equal("some next page regex", definition.NextPageRegex);
            Assert.Equal(2, definition.Captures.Count);
            Assert.Equal("capture1", definition.Captures[0].Name);
            Assert.Equal("capture1 content", definition.Captures[0].Content);
            Assert.Equal("capture2", definition.Captures[1].Name);
            Assert.Equal("capture2 content", definition.Captures[1].Content);
            Assert.Equal("rename pattern", definition.RenamePattern);
        }

        [Fact(Skip="skipped until the new definition format is stable")]
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
            Assert.Equal("some friendly name", definition.Name);
            Assert.Equal(false, definition.AllowMultipleStrips);
            Assert.Equal(false, definition.AllowMissingStrips);
            Assert.Null(definition.Author);
            Assert.Equal("some base url", definition.HomePage);
            Assert.Null(definition.RootUrl);
            Assert.Equal("some comic regex", definition.ComicRegex);
            Assert.Equal("some back button regex", definition.NextPageRegex);
            Assert.Equal(0, definition.Captures.Count);
            Assert.Null(definition.RenamePattern);
        }

		[Fact(Skip = "skipped until the new definition format is stable")]
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

		[Fact(Skip = "skipped until the new definition format is stable")]
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
