using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using MbUnit.Framework;

using Woofy.Updates;

namespace UnitTests
{
    [TestFixture]
    public class UpdateDescriptionTest
    {
        [Test]
        public void TestProperlyLoadsDescription()
        {
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<updateDescription>
    <woofy>
        <release versionNumber=""1.0"">
            <changes>
                <change>First change.</change>
                <change>Second change.</change>
                <change>Third change.</change>
            </changes>
        </release>
        <release versionNumber=""1.0rc1"">
            <changes>
                <change>One change to rule them all.</change>
            </changes>
        </release>
        <release versionNumber=""1.0a"">
        </release>
    </woofy>
    <comicPack>
        <release versionNumber=""1.0.5"">
            <changes>
                <change>First comic change.</change>
                <change>Second comic change.</change>
            </changes>
        </release>
    </comicPack>
</updateDescription>
"));
            UpdateDescription description = new UpdateDescription(stream);

            Assert.IsNotNull(description.Woofy);
            Assert.IsNotNull(description.ComicPack);

            Assert.AreEqual(3, description.Woofy.Count);
            Assert.AreEqual(1, description.ComicPack.Count);

            Assert.AreEqual("1.0", description.Woofy[0].VersionNumber);
            Assert.AreEqual("1.0rc1", description.Woofy[1].VersionNumber);
            Assert.AreEqual("1.0a", description.Woofy[2].VersionNumber);
            Assert.AreEqual("1.0.5", description.ComicPack[0].VersionNumber);

            Assert.AreEqual(3, description.Woofy[0].Changes.Count);
            Assert.AreEqual(1, description.Woofy[1].Changes.Count);
            Assert.AreEqual(0, description.Woofy[2].Changes.Count);
            Assert.AreEqual(2, description.ComicPack[0].Changes.Count);

            Assert.AreEqual("First change.", description.Woofy["1.0"].Changes[0]);
            Assert.AreEqual("Second change.", description.Woofy["1.0"].Changes[1]);
            Assert.AreEqual("Third change.", description.Woofy["1.0"].Changes[2]);
            Assert.AreEqual("One change to rule them all.", description.Woofy["1.0RC1"].Changes[0]);
            Assert.AreEqual("First comic change.", description.ComicPack["1.0.5"].Changes[0]);
            Assert.AreEqual("Second comic change.", description.ComicPack["1.0.5"].Changes[1]);
        }

    }
}
