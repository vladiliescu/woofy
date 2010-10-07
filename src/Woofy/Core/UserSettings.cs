using System.IO;
using Newtonsoft.Json;

namespace Woofy.Core
{
	public interface IUserSettings
	{
		string LastUsedComicDefinitionFile { get; set; }
		long? LastNumberOfComicsToDownload { get; set; }
		string ProxyAddress { get; set; }
		int? ProxyPort { get; set; }
		bool MinimizeToTray { get; set; }
		string DefaultDownloadFolder { get; set; }
		bool AutomaticallyCheckForUpdates { get; set; }
		string LastReportedWoofyVersion { get; set; }
		bool CloseWhenAllComicsHaveFinishedDownloading { get; set; }
		string ProxyUsername { get; set; }
		string ProxyPassword { get; set; }
		bool ShowAdvancedComicOptions { get; set; }

		void Save();
		void Load();
		void CopyAttributesFrom(IUserSettings settings);
	}

	public class UserSettings : IUserSettings
	{
		public string LastUsedComicDefinitionFile { get; set; }
		public long? LastNumberOfComicsToDownload { get; set; }
		public string ProxyAddress { get; set; }
		public int? ProxyPort { get; set; }
		public bool MinimizeToTray { get; set; }
		public string DefaultDownloadFolder { get; set; }
		public bool AutomaticallyCheckForUpdates { get; set; }
		public string LastReportedWoofyVersion { get; set; }
		public bool CloseWhenAllComicsHaveFinishedDownloading { get; set; }
		public string ProxyUsername { get; set; }
		public string ProxyPassword { get; set; }
		public bool ShowAdvancedComicOptions { get; set; }

		readonly IAppSettings appSettings;

		/// <summary>
		/// Needed by Json.NET.
		/// </summary>
		public UserSettings()
		{
		}

		public UserSettings(IAppSettings appSettings)
		{
			this.appSettings = appSettings;

			Load();
		}

		public void Save()
		{
			File.WriteAllText(appSettings.UserSettingsFile, JsonConvert.SerializeObject(this, Formatting.Indented));
		}

		public void Load()
		{
			var rawSettings = File.Exists(appSettings.UserSettingsFile) ? File.ReadAllText(appSettings.UserSettingsFile) : "";
			var settings = JsonConvert.DeserializeObject<UserSettings>(rawSettings) ?? appSettings.DefaultSettings;

			CopyAttributesFrom(settings);
		}

		public void CopyAttributesFrom(IUserSettings settings)
		{
			LastUsedComicDefinitionFile = settings.LastUsedComicDefinitionFile;
			LastNumberOfComicsToDownload = settings.LastNumberOfComicsToDownload;
			ProxyAddress = settings.ProxyAddress;
			ProxyPort = settings.ProxyPort;
			MinimizeToTray = settings.MinimizeToTray;
			DefaultDownloadFolder = settings.DefaultDownloadFolder;
			AutomaticallyCheckForUpdates = settings.AutomaticallyCheckForUpdates;
			LastReportedWoofyVersion = settings.LastReportedWoofyVersion;
			CloseWhenAllComicsHaveFinishedDownloading = settings.CloseWhenAllComicsHaveFinishedDownloading;
			ProxyUsername = settings.ProxyUsername;
			ProxyPassword = settings.ProxyPassword;
			ShowAdvancedComicOptions = settings.ShowAdvancedComicOptions;
		}
	}
}