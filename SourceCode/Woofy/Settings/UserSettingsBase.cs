using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;

namespace Woofy.Settings
{
    public abstract class UserSettingsBase
    {
        #region Static Members
        private static SettingsContainer Settings = new SettingsContainer();
        private static Stream TargetStream = null;
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(SettingsContainer));
        #endregion

        #region Public Properties
        [DefaultValue(null)]
        public static string LastUsedComicDefinitionFile
        {
            get { return Settings.LastUsedComicDefinitionFile; }
            set { Settings.LastUsedComicDefinitionFile = value; }
        }

        [DefaultValue(null)]
        public static long? LastNumberOfComicsToDownload
        {
            get { return Settings.LastNumberOfComicsToDownload; }
            set { Settings.LastNumberOfComicsToDownload = value; }
        }

        [DefaultValue(null)]
        public static string ProxyAddress
        {
            get { return Settings.ProxyAddress; }
            set { Settings.ProxyAddress = value; }
        }

        [DefaultValue(null)]
        public static int? ProxyPort
        {
            get { return Settings.ProxyPort; }
            set { Settings.ProxyPort = value; }
        }

        [DefaultValue(true)]
        public static bool MinimizeToTray
        {
            get { return Settings.MinimizeToTray; }
            set { Settings.MinimizeToTray = value; }
        }

        [DefaultValue(null)]
        public static string DefaultDownloadFolder
        {
            get { return Settings.DefaultDownloadFolder; }
            set { Settings.DefaultDownloadFolder = value; }
        }

        [DefaultValue(true)]
        public static bool AutomaticallyCheckForUpdates
        {
            get { return Settings.AutomaticallyCheckForUpdates; }
            set { Settings.AutomaticallyCheckForUpdates = value; }
        }

        [DefaultValue(null)]
        public static string LastReportedWoofyVersion
        {
            get { return Settings.LastReportedWoofyVersion; }
            set { Settings.LastReportedWoofyVersion = value; }
        }

        [DefaultValue(null)]
        public static string LastReportedComicPackVersion
        {
            get { return Settings.LastReportedComicPackVersion; }
            set { Settings.LastReportedComicPackVersion = value; }
        }
        #endregion

        #region Public Methods
        protected static void Save()
        {
            ThrowIfPreconditionsNotFulfilled();
            Serializer.Serialize(TargetStream, Settings);
        }
        
        protected static void Load()
        {
            ThrowIfPreconditionsNotFulfilled();
            Settings = (SettingsContainer)Serializer.Deserialize(TargetStream);
        }

        public static void Reset()
        {
            foreach (PropertyInfo property in typeof(UserSettingsBase).GetProperties())
            {
                DefaultValueAttribute defaultValue = (DefaultValueAttribute)property.GetCustomAttributes(typeof(DefaultValueAttribute), false)[0];
                property.SetValue(null, defaultValue.Value, null);
            }
        }

        #endregion

        #region Helper Methods
        private static void ThrowIfPreconditionsNotFulfilled()
        {
            if (TargetStream == null)
                throw new NullReferenceException("TargetStream cannot be null. Call InitializeTargetStream to initialize it.");
        } 
        #endregion

        #region Stream Initialization
        protected static void InitializeTargetStream(Stream stream)
        {
            TargetStream = stream;
        } 
        #endregion

        #region SettingsContainer

        public class SettingsContainer
        {
            private string lastUsedComicDefinitionFile;
            public string LastUsedComicDefinitionFile
            {
                get { return this.lastUsedComicDefinitionFile; }
                set { this.lastUsedComicDefinitionFile = value; }
            }

            private long? lastNumberOfComicsToDownload;
            public long? LastNumberOfComicsToDownload
            {
                get { return this.lastNumberOfComicsToDownload; }
                set { this.lastNumberOfComicsToDownload = value; }
            }

            private string proxyAddress;
            public string ProxyAddress
            {
                get { return this.proxyAddress; }
                set { this.proxyAddress = value; }
            }

            private int? proxyPort;
            public int? ProxyPort
            {
                get { return this.proxyPort; }
                set { this.proxyPort = value; }
            }

            private bool minimizeToTray;
            public bool MinimizeToTray
            {
                get { return this.minimizeToTray; }
                set { this.minimizeToTray = value; }
            }

            private string defaultDownloadFolder;
            public string DefaultDownloadFolder
            {
                get { return this.defaultDownloadFolder; }
                set { this.defaultDownloadFolder = value; }
            }

            private bool automaticallyCheckForUpdates;
            public bool AutomaticallyCheckForUpdates
            {
                get { return this.automaticallyCheckForUpdates; }
                set { this.automaticallyCheckForUpdates = value; }
            }

            private string lastReportedWoofyVersion;
            public string LastReportedWoofyVersion
            {
                get { return this.lastReportedWoofyVersion; }
                set { this.lastReportedWoofyVersion = value; }
            }

            private string lastReportedComicPackVersion;
            public string LastReportedComicPackVersion
            {
                get { return this.lastReportedComicPackVersion; }
                set { this.lastReportedComicPackVersion = value; }
            }

        }
        #endregion
    }
}
