using System;
using System.IO;
using System.Reflection;

namespace Woofy.Core
{
    [Obsolete("Use IAppSettings instead.")]
	public static class AppSettingsOld
	{
		public static readonly string VersionNumber = Assembly.GetAssembly(typeof(Program)).GetName().Version.ToString();
		public static readonly string UserSettingsFile = BaseDirectory("user.settings");
		public static readonly string HomePage = "http://code.google.com/p/woofy/";
		public static readonly string AuthorHomePage = "http://vladiliescu.ro";

		private static string BaseDirectory(string fileName)
		{
			return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
		}
	}

	public interface IAppSettings
	{
		Uri UpdateInfoAddress { get; }
		string ComicsFile { get; }
		string UserSettingsFile { get; }
		string ComicDefinitionsFolder { get; }
		string HomePage { get; }
		string AuthorHomePage { get; }
		string ContentGroupName { get; }
		IUserSettings DefaultSettings { get; }
	}

	/// <summary>
	/// This will eventually replace the old static AppSettingsOld class.
	/// </summary>
	public class AppSettings : IAppSettings
	{
		public string VersionNumber { get; private set; }
		public Uri UpdateInfoAddress { get; private set; }
		public string ComicsFile { get; private set; }
		public string UserSettingsFile { get; private set; }
		public string ComicDefinitionsFolder { get; private set; }
		public string HomePage { get; private set; }
		public string AuthorHomePage { get; private set; }
		public string ContentGroupName { get; private set; }
		public IUserSettings DefaultSettings { get; private set; }

		public AppSettings()
		{
			UpdateInfoAddress = new Uri("http://wiki.woofy.googlecode.com/hg/content/updateInfo.json");
			ComicsFile = BaseDirectory("comics.json");

			UserSettingsFile = BaseDirectory("user.settings");
			ComicDefinitionsFolder = BaseDirectory("definitions");

			HomePage = "http://code.google.com/p/woofy/";
			AuthorHomePage = "http://vladiliescu.ro";

			ContentGroupName = "content";

			DefaultSettings = new UserSettings
			{
				LastUsedComicDefinitionFile = null,
				LastNumberOfComicsToDownload = null,
				ProxyAddress = null,
				ProxyPort = null,
				MinimizeToTray = true,
				DefaultDownloadFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Comics"),
				AutomaticallyCheckForUpdates = true,
				AlreadyRejectedApplicationVersion = null,
				CloseWhenAllComicsHaveFinishedDownloading = false,
				ProxyUsername = null,
				ProxyPassword = null
			};
		}

		private static string BaseDirectory(string fileName)
		{
			return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
		}
	}
}