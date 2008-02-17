using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MbUnit.Framework;

using Woofy.Entities;
using Woofy.Services;
using System.IO;

namespace Woofy.Tests
{
    [TestFixture]
    public class ComicDefinitionsServiceTest
    {
        private ComicDefinitionsService _comicDefinitionsService;

        [SetUp]
        public void SetUp()
        {
            _comicDefinitionsService = new ComicDefinitionsService();
        }

        [Test]
        public void ShouldInitializeAllFieldsWithCorrectValues()
        {
            string comicInfoContent = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<comicInfo friendlyName=""some friendly name"" 
            allowMultipleStrips=""true"" 
            allowMissingStrips=""true"" 
            author=""some author""
            authorEmail=""some author email"">
    <comicRegex><![CDATA[some comic regex]]></comicRegex>
    <firstIssue><![CDATA[http://home.page.com/first.png]]></firstIssue>
    <latestPageRegex><![CDATA[some latest page regex]]></latestPageRegex>
    <startUrl><![CDATA[http://home.page.com]]></startUrl>
    <backButtonRegex><![CDATA[some back button regex]]></backButtonRegex>    
</comicInfo>
";
            ComicDefinition definition = _comicDefinitionsService.BuildDefinitionFromStream(new MemoryStream(Encoding.UTF8.GetBytes(comicInfoContent)));

            Assert.AreEqual("some friendly name", definition.Comic.Name);
            Assert.AreEqual(true, definition.AllowMultipleStrips);
            Assert.AreEqual(true, definition.AllowMissingStrips);
            Assert.AreEqual("some author", definition.Author);
            Assert.AreEqual("some author email", definition.AuthorEmail);
            Assert.AreEqual("http://home.page.com/", definition.HomePageAddress.AbsoluteUri);
            Assert.AreEqual("http://home.page.com/first.png", definition.FirstStripAddress.AbsoluteUri);
            Assert.AreEqual("some comic regex", definition.StripRegex);
            Assert.AreEqual("some back button regex", definition.NextIssueRegex);
            Assert.AreEqual("some latest page regex", definition.LatestIssueRegex);
        }
    }
}
