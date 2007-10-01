using System.IO;

namespace Woofy.Settings
{
    public class UserSettingsMemory : UserSettingsBase
    {
        public static void InitializeStream(Stream stream)
        {
            InitializeTargetStream(stream);
        }
    }
}
