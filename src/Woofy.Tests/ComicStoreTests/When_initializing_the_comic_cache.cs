using Moq;
using Woofy.Core;
using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;
using Woofy.Enums;
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

            factory.DefinitionStore.Setup(x => x.Definitions).Returns(new Definition[] { new _AlphaTestDefinition(), new _BetaTestDefinition() });
        }
       
        [Fact]
        public void Should_create_a_comic_for_each_definition()
        {
            factory.File.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns("");

            comicStore.InitializeComicCache();
            var comics = comicStore.Comics;

            Assert.Equal(2, comics.Length);
            Assert.Equal("alpha", comics[0].Name);
            Assert.Equal("beta", comics[1].Name);
        }

        [Fact]
        public void Should_properly_instantiate_comics_based_on_their_definitions()
        {
            factory.File.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns("");
            factory.UserSettings.Setup(x => x.DefaultDownloadFolder).Returns("d:\\comics");

            comicStore.InitializeComicCache();
            var comics = comicStore.Comics;

            Assert.Equal(2, comics.Length);
            var comic = comics[0];
            Assert.Equal("AlphaTestDefinition", comic.DefinitionId);
            Assert.NotNull(comic.Definition);
            Assert.Equal("alpha", comic.Name);
            Assert.Equal("d:\\comics\\AlphaTestDefinition", comic.DownloadFolder);
            Assert.Equal(null, comic.CurrentUrl);
            Assert.Equal(false, comic.IsActive);
            Assert.Equal(DownloadOutcome.None, comic.DownloadOutcome);
            Assert.Equal(TaskStatus.Running, comic.Status);
        }

        [Fact]
        public void Should_also_use_the_persisted_comics_if_available()
        {
            factory.File.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns(@"
[
    {
        ""DefinitionId"": ""AlphaTestDefinition"",
        ""DownloadOutcome"": 2,
        ""Name"": ""not alpha"",
        ""DownloadedComics"": 10,
        ""DownloadFolder"": ""alpha"",
        ""Status"": 2,
        ""CurrentUrl"": ""http://example.com/54"",
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
            factory.File.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns(@"
[
    {
        ""DefinitionId"": ""AlphaTestDefinition"",
        ""DownloadOutcome"": 2,
        ""Name"": ""not alpha"",
        ""DownloadedComics"": 10,
        ""DownloadFolder"": ""alpha"",
        ""Status"": 2,
        ""CurrentUrl"": ""http://example.com/54"",
        ""IsActive"": true
    }
]");
            comicStore.InitializeComicCache();
            Assert.Equal(2, comicStore.Comics.Length);
            var comic = comicStore.Comics[0];
            Assert.Equal("AlphaTestDefinition", comic.DefinitionId);
            Assert.NotNull(comic.Definition);
            Assert.Equal(DownloadOutcome.MultipleStripMatchesRuleBroken, comic.DownloadOutcome);
            Assert.Equal("not alpha", comic.Name);
            Assert.Equal("alpha", comic.DownloadFolder);
            Assert.Equal(TaskStatus.Finished, comic.Status);
            Assert.Equal("http://example.com/54", comic.CurrentUrl);
            Assert.Equal(true, comic.IsActive);
        }
    }
}