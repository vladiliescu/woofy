using System;
using System.IO;
using Newtonsoft.Json;
using Woofy.Core;

namespace Woofy.Settings
{
    [Obsolete("Use IUserSettings instead.")]
	public class UserSettingsOld
	{
		private static readonly UserSettingsData DefaultSettings = new UserSettingsData
		{
			LastUsedComicDefinitionFile = null,
			LastNumberOfComicsToDownload = null,
			ProxyAddress = null,
			ProxyPort = null,
			MinimizeToTray = true,
			DefaultDownloadFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Comics"),
			AutomaticallyCheckForUpdates = true,
			LastReportedWoofyVersion = null,
			CloseWhenAllComicsHaveFinishedDownloading = false,
			ProxyUsername = null,
			ProxyPassword = null

		};

		private static UserSettingsData currentSettings;

		
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

		public static bool CloseWhenAllComicsHaveFinished
		{
			get { return currentSettings.CloseWhenAllComicsHaveFinishedDownloading; }
			set { currentSettings.CloseWhenAllComicsHaveFinishedDownloading = value; }
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
			File.WriteAllText(AppSettingsOld.UserSettingsFile, JsonConvert.SerializeObject(currentSettings, Formatting.Indented));
		}

		public static void LoadData()
		{
			currentSettings = JsonConvert.DeserializeObject<UserSettingsData>(File.ReadAllText(AppSettingsOld.UserSettingsFile));
		}

		private static void EnsureSettingsFileExists()
		{
			if (File.Exists(AppSettingsOld.UserSettingsFile))
				return;


			File.Create(AppSettingsOld.UserSettingsFile).Close();

			currentSettings = DefaultSettings;
			SaveData();
		}
	}
}
