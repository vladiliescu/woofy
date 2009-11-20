using System.IO;
using System.Text;


using Woofy.Settings;
using Xunit;

namespace UnitTests
{
    public class UserSettingsTest
    {
    	public UserSettingsTest()
    	{
			UserSettingsMemory.Reset();
    	}

    	
        [Fact]
        public void TestProperlySavesAndLoadsAllSettings()
        {
            string proxyAddress = "proxy address";
            int? proxyPort = 2090;
            bool minimizeToTray = true;
            long? lastNumberOfComicsToDownload = 5;
            string defaultDownloadFolder = "default download folder";
            bool automaticallyCheckForUpdates = false;

            MemoryStream stream = new MemoryStream();
            UserSettingsMemory.InitializeStream(stream);

            UserSettingsMemory.ProxyAddress = proxyAddress;
            UserSettingsMemory.ProxyPort = proxyPort;
            UserSettingsMemory.MinimizeToTray = minimizeToTray;
            UserSettingsMemory.LastNumberOfComicsToDownload = lastNumberOfComicsToDownload;
            UserSettingsMemory.DefaultDownloadFolder = defaultDownloadFolder;
            UserSettingsMemory.AutomaticallyCheckForUpdates = automaticallyCheckForUpdates;

            stream.Position = 0;
            UserSettingsMemory.SaveData();

            UserSettingsMemory.ProxyAddress = "seriously";
            UserSettingsMemory.ProxyPort = 12;
            UserSettingsMemory.MinimizeToTray = false;
            UserSettingsMemory.LastNumberOfComicsToDownload = 1;
            UserSettingsMemory.DefaultDownloadFolder = "aaaa";
            UserSettingsMemory.AutomaticallyCheckForUpdates = true;

            stream.Position = 0;
            UserSettingsMemory.LoadData();

            Assert.Equal(proxyAddress, UserSettingsMemory.ProxyAddress);
            Assert.Equal(proxyPort, UserSettingsMemory.ProxyPort);
            Assert.Equal(minimizeToTray, UserSettingsMemory.MinimizeToTray);
            Assert.Equal(lastNumberOfComicsToDownload, UserSettingsMemory.LastNumberOfComicsToDownload);
            Assert.Equal(defaultDownloadFolder, UserSettingsMemory.DefaultDownloadFolder);
            Assert.Equal(automaticallyCheckForUpdates, UserSettingsMemory.AutomaticallyCheckForUpdates);
        }

        [Fact]
        public void TestResetSetsMembersToDefaultValues()
        {
            string savedSettings = @"<?xml version=""1.0""?>
<SettingsContainer xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <LastNumberOfComicsToDownload>5</LastNumberOfComicsToDownload>
  <ProxyAddress>proxy address</ProxyAddress>
  <ProxyPort>2090</ProxyPort>
  <MinimizeToTray>true</MinimizeToTray>
</SettingsContainer>";

            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(savedSettings));
            UserSettingsMemory.InitializeStream(stream);

            UserSettingsMemory.Reset();

            Assert.Null(UserSettingsMemory.LastNumberOfComicsToDownload);
            Assert.Null(UserSettingsMemory.ProxyAddress);
            Assert.Null(UserSettingsMemory.ProxyPort);
            Assert.True(UserSettingsMemory.MinimizeToTray);
        }

        [Fact]
        public void TestDoesntCrashOnMissingMember()
        {
            string savedSettings = @"<?xml version=""1.0""?>
<SettingsContainer xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <LastNumberOfComicsToDownload>5</LastNumberOfComicsToDownload>
  <ProxyAddress>proxy address</ProxyAddress>
  <MinimizeToTray>true</MinimizeToTray>
</SettingsContainer>";

            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(savedSettings));
            UserSettingsMemory.InitializeStream(stream);
            UserSettingsMemory.LoadData();

            Assert.Equal(5, UserSettingsMemory.LastNumberOfComicsToDownload);
            Assert.Equal("proxy address", UserSettingsMemory.ProxyAddress);
            Assert.Equal(null, UserSettingsMemory.ProxyPort);
            Assert.Equal(true, UserSettingsMemory.MinimizeToTray);
        }

        [Fact]
        public void TestDoesntCrashOnUnknownMember()
        {
            string savedSettings = @"<?xml version=""1.0""?>
<SettingsContainer xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <LastNumberOfComicsToDownload>5</LastNumberOfComicsToDownload>
  <ProxyAddress>proxy address</ProxyAddress>
  <FuzzyWuzzy>was a bear</FuzzyWuzzy>
  <MinimizeToTray>true</MinimizeToTray>
</SettingsContainer>";

            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(savedSettings));
            UserSettingsMemory.InitializeStream(stream);
            UserSettingsMemory.LoadData();

            Assert.Equal(5, UserSettingsMemory.LastNumberOfComicsToDownload);
            Assert.Equal("proxy address", UserSettingsMemory.ProxyAddress);
            Assert.Equal(null, UserSettingsMemory.ProxyPort);
            Assert.Equal(true, UserSettingsMemory.MinimizeToTray);
        }
    }
}
