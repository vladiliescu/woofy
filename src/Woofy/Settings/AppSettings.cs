using System;
using System.IO;

namespace Woofy.Settings
{
    public static class AppSettings
    {
        public static readonly string VersionNumber = "0.5";

		public static readonly string UpdateDescriptionFileAddress = "http://woofy.sourceforge.net/updatesDescriptionFile.xml";
		public static readonly string DatabaseConnectionString = BaseDirectory("comics.json");

        public static readonly string UserSettingsFile = BaseDirectory("user.settings");
        public static readonly string ComicDefinitionsFolder = BaseDirectory("definitions");


		private static string BaseDirectory(string fileName)
		{
			return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
		}
    }
}
