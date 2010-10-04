using System;
using System.Windows.Forms;
using Woofy.Core;
using Woofy.Gui;

namespace Woofy
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
			Bootstrapper.BootstrapApplication();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.Run(new MainForm());
        }

        /// <summary>
        /// Log exceptions on the additional threads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.LogException((Exception)e.ExceptionObject);
        }

        /// <summary>
        /// Log exceptions on the main UI thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Logger.LogException(e.Exception);
        }
    }
}