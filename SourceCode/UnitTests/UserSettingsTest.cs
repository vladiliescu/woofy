using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using MbUnit.Framework;

using Woofy.Core;
using Woofy.Settings;

namespace UnitTests
{    
    [TestFixture]
    public class UserSettingsTest
    {
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

            UserSettingsMemory.Save();

            UserSettingsMemory.ProxyAddress = "seriously";
            UserSettingsMemory.ProxyPort = 12;
            UserSettingsMemory.MinimizeToTray = false;
            UserSettingsMemory.LastNumberOfComicsToDownload = 1;

            UserSettingsMemory.Load();

            Assert.AreEqual(proxyAddress, UserSettingsMemory.ProxyAddress);
            Assert.AreEqual(proxyPort, UserSettingsMemory.ProxyPort);
            Assert.AreEqual(minimizeToTray, UserSettingsMemory.MinimizeToTray);
            Assert.AreEqual(lastNumberOfComicsToDownload, UserSettingsMemory.LastNumberOfComicsToDownload);
        }

        [Test]
        public void TestUpgradeWorks()
        {
            Assert.IsTrue(false);
        }
    }
}
