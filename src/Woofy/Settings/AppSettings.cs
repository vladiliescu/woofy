using System;
using System.IO;
using System.Reflection;

namespace Woofy.Settings
{
    public static class AppSettings
    {
        public static readonly string VersionNumber = Assembly.GetAssembly(typeof(Program)).GetName().Version.ToString();

		public static readonly string UpdateDescriptionFileAddress = "http://woofy.googlecode.com/hg/serve/updatesDescriptionFile.xml";
		public static readonly string DatabaseConnectionString = BaseDirectory("comics.json");

        public static readonly string UserSettingsFile = BaseDirectory("user.settings");
        public static readonly string ComicDefinitionsFolder = BaseDirectory("definitions");

		public static readonly string HomePage = "http://code.google.com/p/woofy/";
		public static readonly string AuthorHomePage = "http://vladiliescu.ro";


		private static string BaseDirectory(string fileName)
		{
			return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
		}
    }
}
