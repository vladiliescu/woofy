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
		public static SynchronizationContext SynchronizationContext { get; private set; }

        [STAThread]
        static void Main()
        {
			Bootstrapper.BootstrapApplication();          

            //Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

			var mainForm = new MainForm();
			//the synchronization context only becomes available after creating the form
			SynchronizationContext = SynchronizationContext.Current;	
			//the BotSupervisor needs the SynchronizationContext, so I resolve it only after initializing the context
			mainForm.Controller = ContainerAccesor.Resolve<IMainController>();

            Application.Run(mainForm);
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