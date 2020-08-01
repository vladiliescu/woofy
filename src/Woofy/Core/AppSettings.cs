using System;
using System.Configuration;
using System.IO;
using Woofy.Core.SystemProxies;
using Woofy.Core;

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
	    string ExifToolPath { get; }
	    Guid ApplicationGuid { get; }
	    string DefinitionsAssemblyPath { get; }
	}

	/// <summary>
	/// This has eventually replaced the old static AppSettingsOld class.
	/// </summary>
	public class AppSettings : IAppSettings
	{
		public Uri UpdateInfoAddress { get; private set; }
	    public string HomePage { get; private set; }
	    public string AuthorHomePage { get; private set; }
	    public Guid ApplicationGuid { get; private set; }

	    public string ContentGroupName { get; private set; }

	    public string ComicsFile { get; private set; }
	    public string UserSettingsFile { get; private set; }
	    public string ComicDefinitionsFolder { get; private set; }
	    public string ExifToolPath { get; private set; }
	    public string DefinitionsAssemblyPath {get; private set; }

	    public IUserSettings DefaultSettings { get; private set; }
        
	    private readonly bool isPortable = false;
		private readonly IDirectoryProxy directory;

		public AppSettings(IDirectoryProxy directory)
		{
			this.directory = directory;

            isPortable = ConfigurationManager.AppSettings["isPortable"].ParseAsSafe<bool>();

			UpdateInfoAddress = new Uri("https://vladiliescu.net/woofy/updateInfo.json");
            HomePage = "https://vladiliescu.net/woofy";
            AuthorHomePage = "https://vladiliescu.net";
            ApplicationGuid = new Guid("C59EAB54-6C2C-41a0-B516-55452A5AB3D2");

            ContentGroupName = "content";

			ComicsFile = AppSettingsDirectory("comics.json");
			UserSettingsFile = AppSettingsDirectory("user.settings");
			ComicDefinitionsFolder = BaseDirectory("definitions");
            ExifToolPath = BaseDirectory("exiftool.exe");
            DefinitionsAssemblyPath = AppSettingsDirectory("Definitions.dll");

			DefaultSettings = new UserSettings
			{
				MinimizeToTray = true,
				DownloadFolder = ContentFolder("My Comics"),
				AutomaticallyCheckForUpdates = true,
				AlreadyRejectedApplicationVersion = null,
				CloseWhenAllComicsHaveFinishedDownloading = false,
			};
		}

	    private string ContentFolder(string content)
	    {
            if (isPortable)
                return BaseDirectory(content);

            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), content);
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