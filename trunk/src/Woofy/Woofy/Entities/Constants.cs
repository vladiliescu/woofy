using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Woofy.Entities
{
    public static class Constants
    {
        public const string ContentGroup = "content";
        public const string FaviconRegex = @"<link\srel=""shortcut\sicon""\shref=""(?<content>[\w./:]*)""";
        public static readonly RegexOptions RegexOptions = RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace;
        
        //Folders
        public static readonly string DefaultDownloadFolder = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string FaviconsFolder = Path.Combine(DefaultDownloadFolder, "favicons");
        public static readonly string TempFolder = Path.GetTempPath();


        /// <summary>
        /// Specifies the maximum size, in bytes, of the download buffer.
        /// </summary>
        public const int DownloadBufferSize = 16384;
    }
}
