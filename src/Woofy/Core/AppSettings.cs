using System;
using System.IO;
using System.Reflection;

namespace Woofy.Core
{
	public static class AppSettingsOld
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

	public interface IAppSettings
	{
		string VersionNumber { get; set; }
		string UpdateDescriptionFileAddress { get; set; }
		string ComicsFile { get; set; }
		string UserSettingsFile { get; set; }
		string ComicDefinitionsFolder { get; set; }
		string HomePage { get; set; }
		string AuthorHomePage { get; set; }
	}

	/// <summary>
	/// This will eventually replace the old static AppSettingsOld class.
	/// </summary>
	public class AppSettings : IAppSettings
	{
		public string VersionNumber { get; set; }

		public string UpdateDescriptionFileAddress { get; set; }

		public string ComicsFile { get; set; }

		public string UserSettingsFile { get; set; }

		public string ComicDefinitionsFolder { get; set; }

		public string HomePage { get; set; }

		public string AuthorHomePage { get; set; }

		public AppSettings()
		{
			VersionNumber = Assembly.GetAssembly(typeof(Program)).GetName().Version.ToString();

			UpdateDescriptionFileAddress = "http://woofy.googlecode.com/hg/serve/updatesDescriptionFile.xml";
			ComicsFile = BaseDirectory("comics.json");

			UserSettingsFile = BaseDirectory("user.settings");
			ComicDefinitionsFolder = BaseDirectory("definitions");

			HomePage = "http://code.google.com/p/woofy/";
			AuthorHomePage = "http://vladiliescu.ro";
		}

		private static string BaseDirectory(string fileName)
		{
			return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
		}
	}
}