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
        private ComicDefinitionsServiceStub _comicDefinitionsServiceStub;

        [SetUp]
        public void SetUp()
        {
            _comicDefinitionsService = new ComicDefinitionsService();
            _comicDefinitionsServiceStub = new ComicDefinitionsServiceStub();
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
            Comic comic = _comicDefinitionsService.BuildComicFromDefinition(new MemoryStream(Encoding.UTF8.GetBytes(comicInfoContent)));

            Assert.AreEqual("some friendly name", comic.Name);
            Assert.AreEqual(true, comic.AllowMultipleStrips);
            Assert.AreEqual(true, comic.AllowMissingStrips);
            Assert.AreEqual("some author", comic.DefinitionAuthor);
            Assert.AreEqual("some author email", comic.DefinitionAuthorEmail);
            Assert.AreEqual("http://home.page.com/", comic.HomePageUrl.AbsoluteUri);
            Assert.AreEqual("http://home.page.com/first.png", comic.FirstStripUrl.AbsoluteUri);
            Assert.AreEqual("some comic regex", comic.StripRegex);
            Assert.AreEqual("some back button regex", comic.NextIssueRegex);
            Assert.AreEqual("some latest page regex", comic.LatestIssueRegex);
        }

        [Test]
        public void ShouldBuildAComicForEachDefinitionPassed()
        {
            string[] definitionFileNames = new string[5];
            _comicDefinitionsServiceStub.BuildComicsFromDefinitions(definitionFileNames);

            Assert.AreEqual(definitionFileNames.Length, _comicDefinitionsServiceStub.TimesBuildComicFromDefinitionCalled);
        }
    }
}
