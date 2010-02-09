using Woofy.Settings;

namespace Woofy.Core
{
	public static class Bootstrapper
	{
		public static void BootstrapApplication()
		{
			UserSettings.Initialize();
		}
	}
}