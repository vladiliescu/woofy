using System;
using System.Threading;
using System.Windows.Forms;
using Woofy.Core;
using Woofy.Core.Infrastructure;
using Woofy.Gui.Main;

namespace Woofy
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
			Bootstrapper.BootstrapApplication();          

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.Run(new MainForm(ContainerAccesor.Resolve<IMainController>()));
			//Application.Run(new ComicSelectionForm(ContainerAccesor.Container.Resolve<IComicSelectionController>()));
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
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
			//TODO: it looks like this event is preventing exceptions from reaching the user - should i handle these exceptions differently?
            Logger.LogException(e.Exception);
        }
    }
}