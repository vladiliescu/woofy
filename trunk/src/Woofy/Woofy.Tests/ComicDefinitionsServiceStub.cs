using System;
using System.IO;

using Woofy.Entities;
using Woofy.Services;

namespace Woofy.Tests
{
    public class ComicDefinitionsServiceStub : ComicDefinitionsService
    {
        public int TimesBuildComicFromDefinitionCalled { get; private set; }

        public override Comic BuildComicFromDefinition(Stream comicInfoStream)
        {
            TimesBuildComicFromDefinitionCalled++;

            return null;
        }

        public override Comic BuildComicFromDefinition(string definitionFile)
        {
            return BuildComicFromDefinition(Stream.Null);
        }
    }
}
