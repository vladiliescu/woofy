using System;
using System.Text;
using System.IO;

using Woofy.Updates;
using Xunit;

namespace UnitTests
{

    public class UpdateDescriptionTest
    {
        [Fact]
        public void TestProperlyLoadsDescription()
        {
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<updateDescription>
    <woofy>
        <release versionNumber=""1.0"" downloadAddress=""First download address."" releaseDate=""2007-01-04"" size=""1"">
            <changes>
                <change>First change.</change>
                <change>Second change.</change>
                <change>Third change.</change>
            </changes>
        </release>
        <release versionNumber=""1.0rc1"" downloadAddress=""Second download address."" releaseDate=""2007-01-03"" size=""2"">
            <changes>
                <change>One change to rule them all.</change>
            </changes>
        </release>
        <release versionNumber=""1.0a"" downloadAddress=""Third download address."" releaseDate=""2007-01-02"" size=""3"">
        </release>
    </woofy>
    <comicPack>
        <release versionNumber=""1.0.5"" downloadAddress=""Fourth download address."" releaseDate=""2007-01-07 23:32:32Z"" size=""4"">
            <changes>
                <change>First comic change.</change>
                <change>Second comic change.</change>
            </changes>
        </release>
    </comicPack>
</updateDescription>
"));
            UpdateDescription description = new UpdateDescription(stream);

            Assert.NotNull(description.Woofy);
            Assert.NotNull(description.ComicPack);

            Assert.Equal(3, description.Woofy.Count);
            Assert.Equal(1, description.ComicPack.Count);

            Assert.Equal("1.0", description.Woofy[0].VersionNumber);
            Assert.Equal("1.0rc1", description.Woofy[1].VersionNumber);
            Assert.Equal("1.0a", description.Woofy[2].VersionNumber);
            Assert.Equal("1.0.5", description.ComicPack[0].VersionNumber);

            Assert.Equal(3, description.Woofy[0].Changes.Count);
            Assert.Equal(1, description.Woofy[1].Changes.Count);
            Assert.Equal(0, description.Woofy[2].Changes.Count);
            Assert.Equal(2, description.ComicPack[0].Changes.Count);

            Assert.Equal("First change.", description.Woofy["1.0"].Changes[0]);
            Assert.Equal("Second change.", description.Woofy["1.0"].Changes[1]);
            Assert.Equal("Third change.", description.Woofy["1.0"].Changes[2]);
            Assert.Equal("One change to rule them all.", description.Woofy["1.0RC1"].Changes[0]);
            Assert.Equal("First comic change.", description.ComicPack["1.0.5"].Changes[0]);
            Assert.Equal("Second comic change.", description.ComicPack["1.0.5"].Changes[1]);

            Assert.Equal("First download address.", description.Woofy["1.0"].DownloadAddress);
            Assert.Equal("Second download address.", description.Woofy["1.0rc1"].DownloadAddress);
            Assert.Equal("Third download address.", description.Woofy["1.0a"].DownloadAddress);
            Assert.Equal("Fourth download address.", description.ComicPack["1.0.5"].DownloadAddress);

            Assert.Equal(new DateTime(2007, 1, 4), description.Woofy["1.0"].ReleaseDate);
            Assert.Equal(new DateTime(2007, 1, 3), description.Woofy["1.0rc1"].ReleaseDate);
            Assert.Equal(new DateTime(2007, 1, 2), description.Woofy["1.0a"].ReleaseDate);
            Assert.Null(description.ComicPack["1.0.5"].ReleaseDate);
        }

    }
}
