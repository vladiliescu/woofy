using System;
using System.Threading;
using System.Windows.Forms;
using Woofy.Core;
using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;
using Woofy.Gui.CompilationError;
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

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => Logger.LogException((Exception)e.ExceptionObject);

			CompileDefinitions();

			var mainForm = new MainForm();
			//the synchronization context only becomes available after creating the form
			SynchronizationContext = SynchronizationContext.Current;	
			//the BotSupervisor needs the SynchronizationContext, so I resolve it only after initializing the context
			mainForm.Controller = ContainerAccessor.Resolve<IMainController>();

            Application.Run(mainForm);
        }

    	private static void CompileDefinitions()
    	{
			var definitionStore = ContainerAccessor.Resolve<IDefinitionStore>();
			var compilationErrorController = ContainerAccessor.Resolve<ICompilationErrorController>();

			do
			{
				try
				{
					definitionStore.InitializeDefinitionCache();
					return;
				}
				catch (CompilationException ex)
				{
					var shouldRetry = compilationErrorController.DisplayError(ex);
					if (!shouldRetry)
					{
						Environment.Exit(1);
						return;
					}
				}
			}
			while (true);
    	}
    }
}
