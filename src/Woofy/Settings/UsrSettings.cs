using System.IO;
using Newtonsoft.Json;

namespace Woofy.Settings
{
	public class UsrSettings
	{
		private static readonly UserSettings defaultSettings = new UserSettings()
		{
			LastUsedComicDefinitionFile = null,
			LastNumberOfComicsToDownload = null,
			ProxyAddress = null,
			ProxyPort = null,
			MinimizeToTray = true,
			DefaultDownloadFolder = "",
			AutomaticallyCheckForUpdates = true,
			LastReportedWoofyVersion = null,
			LastReportedComicPackVersion = null,
			CloseWhenAllComicsHaveFinished = false,
			ProxyUsername = null,
			ProxyPassword = null

		};
		private static UserSettings currentSettings;

		public static string LastUsedComicDefinitionFile
		{
			get { return currentSettings.LastUsedComicDefinitionFile; }
			set { currentSettings.LastUsedComicDefinitionFile = value; }
		}

		public static long? LastNumberOfComicsToDownload
		{
			get { return currentSettings.LastNumberOfComicsToDownload; }
			set { currentSettings.LastNumberOfComicsToDownload = value; }
		}

		public static string ProxyAddress
		{
			get { return currentSettings.ProxyAddress; }
			set { currentSettings.ProxyAddress = value; }
		}

		public static int? ProxyPort
		{
			get { return currentSettings.ProxyPort; }
			set { currentSettings.ProxyPort = value; }
		}

		public static bool MinimizeToTray
		{
			get { return currentSettings.MinimizeToTray; }
			set { currentSettings.MinimizeToTray = value; }
		}

		public static string DefaultDownloadFolder
		{
			get { return currentSettings.DefaultDownloadFolder; }
			set { currentSettings.DefaultDownloadFolder = value; }
		}

		public static bool AutomaticallyCheckForUpdates
		{
			get { return currentSettings.AutomaticallyCheckForUpdates; }
			set { currentSettings.AutomaticallyCheckForUpdates = value; }
		}

		public static string LastReportedWoofyVersion
		{
			get { return currentSettings.LastReportedWoofyVersion; }
			set { currentSettings.LastReportedWoofyVersion = value; }
		}

		public static string LastReportedComicPackVersion
		{
			get { return currentSettings.LastReportedComicPackVersion; }
			set { currentSettings.LastReportedComicPackVersion = value; }
		}

		public static bool CloseWhenAllComicsHaveFinished
		{
			get { return currentSettings.CloseWhenAllComicsHaveFinished; }
			set { currentSettings.CloseWhenAllComicsHaveFinished = value; }
		}

		public static string ProxyUsername
		{
			get { return currentSettings.ProxyUsername; }
			set { currentSettings.ProxyUsername = value; }
		}

		public static string ProxyPassword
		{
			get { return currentSettings.ProxyPassword; }
			set { currentSettings.ProxyPassword = value; }
		}

		public static void Initialize()
		{
			EnsureSettingsFileExists();
			LoadData();
		}

		public static void SaveData()
		{
			File.WriteAllText(AppSettings.UserSettingsFile, JsonConvert.SerializeObject(currentSettings, Formatting.Indented));
		}

		public static void LoadData()
		{
			currentSettings = JsonConvert.DeserializeObject<UserSettings>(File.ReadAllText(AppSettings.UserSettingsFile));
		}

		private static void EnsureSettingsFileExists()
		{
			if (File.Exists(AppSettings.UserSettingsFile))
				return;


			File.Create(AppSettings.UserSettingsFile).Close();

			currentSettings = defaultSettings;
			SaveData();
		}
	}


}
