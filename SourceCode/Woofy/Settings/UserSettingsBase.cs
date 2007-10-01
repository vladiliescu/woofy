using System;
using System.IO;
using System.Xml.Serialization;

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
        public static string LastUsedComicDefinitionFile
        {
            get { return Settings.LastUsedComicDefinitionFile; }
            set { Settings.LastUsedComicDefinitionFile = value; }
        }

        public static long? LastNumberOfComicsToDownload
        {
            get { return Settings.LastNumberOfComicsToDownload; }
            set { Settings.LastNumberOfComicsToDownload = value; }
        }

        public static string ProxyAddress
        {
            get { return Settings.ProxyAddress; }
            set { Settings.ProxyAddress = value; }
        }

        public static int ProxyPort
        {
            get { return Settings.ProxyPort; }
            set { Settings.ProxyPort = value; }
        }

        public static bool MinimizeToTray
        {
            get { return Settings.MinimizeToTray; }
            set { Settings.MinimizeToTray = value; }
        }
        #endregion

        #region Public Methods
        public static void Save()
        {
            ThrowIfPreconditionsNotFulfilled();

            TargetStream.Position = 0;
            Serializer.Serialize(TargetStream, Settings);
        }

        //public static void Reset()
        //{
        //    ThrowIfPreconditionsNotFulfilled();
        //}

        public static void Load()
        {
            ThrowIfPreconditionsNotFulfilled();

            TargetStream.Position = 0;
            Settings = (SettingsContainer)Serializer.Deserialize(TargetStream);
        }

        //public static void Upgrade(Stream stream)
        //{
        //    ThrowIfPreconditionsNotFulfilled();
        //}

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

            private int proxyPort;
            public int ProxyPort
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

        }
        #endregion
    }
}
