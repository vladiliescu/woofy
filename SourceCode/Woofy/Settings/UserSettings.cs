using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Woofy.Settings
{
    public class UserSettings : UserSettingsBase
    {
        public static string LastReportedWoofyVersion;
        public static string LastReportedComicPackVersion;

        static UserSettings()
        {
            FileStream stream = new FileStream(ApplicationSettings.UserSettingsFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            InitializeTargetStream(stream);
        }
    }
}
