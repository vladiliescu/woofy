using System;
using System.IO;

namespace Woofy.Settings
{
    public static class ApplicationSettings
    {
        public const string VersionNumber = "0.4.0";

        public const string UpdateDescriptionFileAddress = "http://localhost/updatesDescriptionFile.xml";
        public const string ComicDefinitionsFolderName = "ComicInfos";
        public const string DatabaseConnectionString = "Data Source=data.s3db;Version=3;";

        public static readonly string UserSettingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user.settings");
        public static readonly string ComicDefinitionsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ComicDefinitionsFolderName);
        
    }
}
