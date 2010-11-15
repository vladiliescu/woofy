using System;
using Moq;
using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;
using Xunit;

namespace Woofy.Tests.ComicStoreTests
{
    public class When_initializing_the_comic_cache
    {
        private readonly ObjectMother factory = new ObjectMother();
        private readonly ComicStore comicStore;

        public When_initializing_the_comic_cache()
        {
            comicStore = factory.CreateComicStore();

            factory.File.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);
            factory.DefinitionStore.Setup(x => x.Definitions).Returns(new Definition[] { new _AlphaTestDefinition(), new _BetaTestDefinition() });
        }
       
        [Fact]
        public void Should_create_a_comic_for_each_definition()
        {
            comicStore.InitializeComicCache();
            var comics = comicStore.Comics;

            Assert.Equal(2, comics.Length);
            Assert.Equal("alpha", comics[0].Name);
            Assert.Equal("beta", comics[1].Name);
        }

        [Fact]
        public void Should_properly_instantiate_comics_based_on_their_definitions()
        {
            factory.UserSettings.Setup(x => x.DownloadFolder).Returns("d:\\comics");

            comicStore.InitializeComicCache();
            var comics = comicStore.Comics;

            Assert.Equal(2, comics.Length);
            var comic = comics[0];
            Assert.Equal("AlphaTestDefinition", comic.Id);
            Assert.NotNull(comic.Definition);
            Assert.Equal("alpha", comic.Name);
            Assert.Equal(null, comic.CurrentPage);
            Assert.Equal(Status.Running, comic.Status);
        }

        [Fact]
        public void Should_give_priority_to_the_persisted_comics()
        {
            factory.File.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            factory.File.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns(@"
[
    {
        ""Id"": ""AlphaTestDefinition"",
        ""DownloadOutcome"": 2,
        ""Name"": ""not alpha"",
        ""DownloadedStrips"": 10,
        ""Status"": 2,
        ""CurrentPage"": ""http://example.com/54"",
        ""IsActive"": true
    }
]");
            comicStore.InitializeComicCache();
            Assert.Equal(2, comicStore.Comics.Length);
            Assert.Equal("not alpha", comicStore.Comics[0].Name);
            Assert.Equal("beta", comicStore.Comics[1].Name);
        }

        [Fact]
        public void Should_parse_the_persisted_comics()
        {
            factory.File.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            factory.File.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns(@"
[
    {
        ""Id"": ""AlphaTestDefinition"",
        ""DownloadOutcome"": 2,
        ""Name"": ""not alpha"",
        ""DownloadedStrips"": 10,
        ""Status"": 2,
        ""CurrentPage"": ""http://example.com/54"",
        ""IsActive"": true
    }
]");
            comicStore.InitializeComicCache();
            Assert.Equal(2, comicStore.Comics.Length);
            var comic = comicStore.Comics[0];
            Assert.Equal("AlphaTestDefinition", comic.Id);
            Assert.NotNull(comic.Definition);
            Assert.Equal("not alpha", comic.Name);
            Assert.Equal(Status.Paused, comic.Status);
            Assert.Equal(new Uri("http://example.com/54"), comic.CurrentPage);
        }
    }
}