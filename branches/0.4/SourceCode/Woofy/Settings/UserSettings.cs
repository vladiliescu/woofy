using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace Woofy.Settings
{
    public class UserSettings : UserSettingsBase
    {
        public static void Initialize()
        {
            if (File.Exists(ApplicationSettings.UserSettingsFile))
            {
                using (FileStream stream = new FileStream(ApplicationSettings.UserSettingsFile, FileMode.Open, FileAccess.ReadWrite))
                {
                    InitializeTargetStream(stream);
                    Load();
                }
            }
            else
            {
                using (FileStream stream = new FileStream(ApplicationSettings.UserSettingsFile, FileMode.Create, FileAccess.ReadWrite))
                {
                    InitializeTargetStream(stream);
                    Reset();
                    DetectInternetExplorerProxy();
                    Save();
                }
            }
        }

        public static void SaveData()
        {
            using (FileStream stream = new FileStream(ApplicationSettings.UserSettingsFile, FileMode.Create, FileAccess.Write))
            {
                InitializeTargetStream(stream);
                Save();
            }
        }

        public static void LoadData()
        {
            using (FileStream stream = new FileStream(ApplicationSettings.UserSettingsFile, FileMode.Open, FileAccess.Read))
            {
                InitializeTargetStream(stream);
                Load();
            }
        }

        private static void DetectInternetExplorerProxy()
        {
            RegistryKey internetSettings = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings");
            int proxyEnabled = (int)internetSettings.GetValue("ProxyEnable", 0);
            if (proxyEnabled == 0)
                return;

            string proxyAddress = (string)internetSettings.GetValue("ProxyServer");
            Match match = Regex.Match(proxyAddress, @"(?<proxyAddress>[\w]*(://)?[\w.]*):?(?<proxyPort>[0-9]*)");
            if (match.Success)
            {
                if (match.Groups["proxyAddress"].Success)
                    ProxyAddress = match.Groups["proxyAddress"].Value;

                if (match.Groups["proxyPort"].Success)
                    ProxyPort = int.Parse(match.Groups["proxyPort"].Value);
            }
        }
    }
}
