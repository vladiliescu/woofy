using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Woofy.Core
{
    public abstract class UserSettingsBase
    {
        #region Static Members
        private static SettingsContainer Settings = new SettingsContainer();
        private static XmlSerializer Serializer = new XmlSerializer(typeof(SettingsContainer));
        private static Stream TargetStream = null;
        #endregion

        #region Public Properties
        public static string LastUsedComicDefinitionFile
        {
            get { return Settings.LastUsedComicDefinitionFile; }
        }

        public static long? LastNumberOfComicsToDownload
        {
            get { return Settings.LastNumberOfComicsToDownload; }
        }

        public static string ProxyAddress
        {
            get { return Settings.ProxyAddress; }
        }

        public static int ProxyPort
        {
            get { return Settings.ProxyPort; }
        }

        public static bool MinimizeToTray
        {
            get { return Settings.MinimizeToTray; }
        }
        #endregion

        #region Public Methods
        public static void Save()
        {
            ThrowIfPreconditionsNotFulfilled();

            Serializer.Serialize(TargetStream, Settings);
        }

        public static void Reset()
        {
            ThrowIfPreconditionsNotFulfilled();
        }

        public static void Upgrade(Stream stream)
        {
            ThrowIfPreconditionsNotFulfilled();
        }

        #endregion

        private static void ThrowIfPreconditionsNotFulfilled()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected static void InitializeTargetStream(Stream stream)
        {
            TargetStream = stream;
        }

        #region SettingsContainer

        private class SettingsContainer
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
