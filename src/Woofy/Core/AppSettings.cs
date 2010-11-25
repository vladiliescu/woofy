using System;
using System.IO;
using Woofy.Core.SystemProxies;

namespace Woofy.Core
{
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
		public Uri UpdateInfoAddress { get; private set; }
		public string ComicsFile { get; private set; }
		public string UserSettingsFile { get; private set; }
		public string ComicDefinitionsFolder { get; private set; }
		public string HomePage { get; private set; }
		public string AuthorHomePage { get; private set; }
		public string ContentGroupName { get; private set; }
		public IUserSettings DefaultSettings { get; private set; }

		private readonly bool isPortable = false;
		private readonly IDirectoryProxy directory;

		public AppSettings(IDirectoryProxy directory)
		{
			this.directory = directory;

			UpdateInfoAddress = new Uri("http://wiki.woofy.googlecode.com/hg/content/updateInfo.json");
			
			ComicsFile = AppSettingsDirectory("comics.json");
			UserSettingsFile = AppSettingsDirectory("user.settings");

			ComicDefinitionsFolder = BaseDirectory("definitions");

			HomePage = "http://code.google.com/p/woofy/";
			AuthorHomePage = "http://vladiliescu.ro";

			ContentGroupName = "content";

			DefaultSettings = new UserSettings
			{
				MinimizeToTray = true,
				DownloadFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Comics"),
				AutomaticallyCheckForUpdates = true,
				AlreadyRejectedApplicationVersion = null,
				CloseWhenAllComicsHaveFinishedDownloading = false,
			};
		}

		private string BaseDirectory(string fileName)
		{
			return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
		}

		private string AppSettingsDirectory(string fileName)
		{
			if (isPortable)
				return BaseDirectory(fileName);

			var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Woofy");
			if (!directory.Exists(folder))
				directory.CreateDirectory(folder);

			return Path.Combine(folder, fileName);
		}
	}
}