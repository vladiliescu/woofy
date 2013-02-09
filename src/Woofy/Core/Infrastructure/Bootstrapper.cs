using System.Windows.Forms;

namespace Woofy.Core.Infrastructure
{
	public static class Bootstrapper
	{
		public static void BootstrapApplication()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            
            ConfigureLogging.Run();
		}
	}
}