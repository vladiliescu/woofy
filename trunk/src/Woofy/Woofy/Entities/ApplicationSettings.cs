using System;
using System.IO;

namespace Woofy.Entities
{
    public static class ApplicationSettings
    {
        public static readonly string ComicDefinitionsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ComicDefinitions");
    }
}
