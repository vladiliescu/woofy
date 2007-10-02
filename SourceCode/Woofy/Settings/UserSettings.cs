using System.IO;

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
    }
}
