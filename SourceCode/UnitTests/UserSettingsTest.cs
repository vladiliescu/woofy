using System.IO;
using System.Text;

using MbUnit.Framework;

using Woofy.Settings;

namespace UnitTests
{
    [TestFixture]
    public class UserSettingsTest
    {
        [SetUp]
        public void ResetMembersToTheirDefaultValues()
        {
            UserSettingsMemory.Reset();
        }

        [Test]
        public void TestProperlySavesAndLoadsAllSettings()
        {
            string proxyAddress = "proxy address";
            int proxyPort = 2090;
            bool minimizeToTray = true;
            long? lastNumberOfComicsToDownload = 5;

            MemoryStream stream = new MemoryStream();
            UserSettingsMemory.InitializeStream(stream);

            UserSettingsMemory.ProxyAddress = proxyAddress;
            UserSettingsMemory.ProxyPort = proxyPort;
            UserSettingsMemory.MinimizeToTray = minimizeToTray;
            UserSettingsMemory.LastNumberOfComicsToDownload = lastNumberOfComicsToDownload;

            stream.Position = 0;
            UserSettingsMemory.Save();

            UserSettingsMemory.ProxyAddress = "seriously";
            UserSettingsMemory.ProxyPort = 12;
            UserSettingsMemory.MinimizeToTray = false;
            UserSettingsMemory.LastNumberOfComicsToDownload = 1;

            stream.Position = 0;
            UserSettingsMemory.Load();

            Assert.AreEqual(proxyAddress, UserSettingsMemory.ProxyAddress);
            Assert.AreEqual(proxyPort, UserSettingsMemory.ProxyPort);
            Assert.AreEqual(minimizeToTray, UserSettingsMemory.MinimizeToTray);
            Assert.AreEqual(lastNumberOfComicsToDownload, UserSettingsMemory.LastNumberOfComicsToDownload);
        }

        [Test]
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

            Assert.IsNull(UserSettingsMemory.LastNumberOfComicsToDownload);
            Assert.IsNull(UserSettingsMemory.ProxyAddress);
            Assert.IsNull(UserSettingsMemory.ProxyPort);
            Assert.IsTrue(UserSettingsMemory.MinimizeToTray);
        }

        [Test]
        public void TestInitializeStreamResetsMembers()
        {
            MemoryStream stream = new MemoryStream();
            UserSettingsMemory.InitializeStream(stream);

            Assert.IsNull(UserSettingsMemory.LastNumberOfComicsToDownload);
            Assert.IsNull(UserSettingsMemory.ProxyAddress);
            Assert.IsNull(UserSettingsMemory.ProxyPort);
            Assert.IsTrue(UserSettingsMemory.MinimizeToTray);
        }

        [Test]
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
            UserSettingsBase.Load();

            Assert.AreEqual(5, UserSettingsMemory.LastNumberOfComicsToDownload);
            Assert.AreEqual("proxy address", UserSettingsMemory.ProxyAddress);
            Assert.AreEqual(null, UserSettingsMemory.ProxyPort);
            Assert.AreEqual(true, UserSettingsMemory.MinimizeToTray);
        }

        [Test]
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
            UserSettingsMemory.Load();

            Assert.AreEqual(5, UserSettingsMemory.LastNumberOfComicsToDownload);
            Assert.AreEqual("proxy address", UserSettingsMemory.ProxyAddress);
            Assert.AreEqual(null, UserSettingsMemory.ProxyPort);
            Assert.AreEqual(true, UserSettingsMemory.MinimizeToTray);
        }
    }
}
