using System.IO;

namespace Woofy.Settings
{
    public class UserSettingsMemory : UserSettingsBase
    {
        private static Stream TargetStream;
        public static void InitializeStream(Stream stream)
        {
            TargetStream = stream;
            InitializeTargetStream(stream);
        }

        public static void SaveData()
        {
            TargetStream.Position = 0;
            Save();
        }

        public static void LoadData()
        {
            TargetStream.Position = 0;
            Load();
        }
    }
}
