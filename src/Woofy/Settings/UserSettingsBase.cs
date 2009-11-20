namespace Woofy.Settings
{
    public class UserSettings
    {
		public string LastUsedComicDefinitionFile { get; set; }
		public long? LastNumberOfComicsToDownload { get; set; }
		public string ProxyAddress { get; set; }
		public int? ProxyPort { get; set; }
		public bool MinimizeToTray { get; set; }
		public string DefaultDownloadFolder { get; set; }
		public bool AutomaticallyCheckForUpdates { get; set; }
		public string LastReportedWoofyVersion { get; set; }
		public string LastReportedComicPackVersion { get; set; }
		public bool CloseWhenAllComicsHaveFinished { get; set; }
		public string ProxyUsername { get; set; }
		public string ProxyPassword { get; set; }
    }
}
