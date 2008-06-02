using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Woofy.Other
{
    public static class ApplicationSettings
    {
        public static readonly string ComicDefinitionsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ComicDefinitions");

        public const string ContentGroup = "content";
        public const string FaviconRegex = @"<link\srel=""shortcut\sicon""\shref=""(?<content>[\w./:]*)""";
        public static readonly RegexOptions RegexOptions = RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace;

        public const string ConnectionString = @"Data Source=..\..\..\..\..\db\data.db3;Version=3;";

        //Folders
        public static readonly string DefaultDownloadFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Comics");
        public static readonly string FaviconsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "favicons");
        public static readonly string TempFolder = Path.GetTempPath();
    }
}