using System;
using System.IO;
using Newtonsoft.Json;

namespace Woofy.Core
{
	public interface IUserSettings
	{
		bool MinimizeToTray { get; set; }
		string DownloadFolder { get; set; }
		bool AutomaticallyCheckForUpdates { get; set; }
		Version AlreadyRejectedApplicationVersion { get; set; }
		bool CloseWhenAllComicsHaveFinishedDownloading { get; set; }

		void Save();
		void Load();
		void CopyAttributesFrom(IUserSettings settings);
	}

	public class UserSettings : IUserSettings
	{
		public bool MinimizeToTray { get; set; }
		public string DownloadFolder { get; set; }
		public bool AutomaticallyCheckForUpdates { get; set; }
		public Version AlreadyRejectedApplicationVersion { get; set; }
		public bool CloseWhenAllComicsHaveFinishedDownloading { get; set; }

		private readonly IAppSettings appSettings;

		private readonly JsonConverter[] converters = new JsonConverter[] { new VersionConverter() };

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
			File.WriteAllText(appSettings.UserSettingsFile, JsonConvert.SerializeObject(this, Formatting.Indented, converters));
		}

		public void Load()
		{
			var rawSettings = File.Exists(appSettings.UserSettingsFile) ? File.ReadAllText(appSettings.UserSettingsFile) : "";
			var settings = JsonConvert.DeserializeObject<UserSettings>(rawSettings, converters) ?? appSettings.DefaultSettings;

			CopyAttributesFrom(settings);
		}

		public void CopyAttributesFrom(IUserSettings settings)
		{
			MinimizeToTray = settings.MinimizeToTray;
			DownloadFolder = settings.DownloadFolder;
			AutomaticallyCheckForUpdates = settings.AutomaticallyCheckForUpdates;
			AlreadyRejectedApplicationVersion = settings.AlreadyRejectedApplicationVersion;
			CloseWhenAllComicsHaveFinishedDownloading = settings.CloseWhenAllComicsHaveFinishedDownloading;
		}
	}
}