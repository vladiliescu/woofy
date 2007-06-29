using System;
using System.Windows.Forms;

using Woofy.Core;
using Woofy.Gui;
using Woofy.Properties;

namespace Woofy
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //if we have just upgraded the application, then get its settings up to date
            if (Settings.Default.GetPreviousVersion("MinimizeToTray") != null)
                Settings.Default.Upgrade();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.Run(new MainForm());
        }

        /// <summary>
        /// Log exceptions on the additional threads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.LogException((Exception) e.ExceptionObject);
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