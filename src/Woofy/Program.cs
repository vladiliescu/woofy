using System;
using System.Threading;
using System.Windows.Forms;
using NLog;
using Woofy.Core;
using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;
using Woofy.Flows.Main;
using Woofy.Flows.Tray;
using Woofy.Gui.CompilationError;

namespace Woofy
{
    static class Program
    {
		public static SynchronizationContext SynchronizationContext { get; private set; }

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
    					Environment.Exit(1);
    			}
    		}
    		while (true);
    	}

    	[STAThread]
        static void Main()
        {
			Bootstrapper.BootstrapApplication();          

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => LogManager.GetLogger("errorLog").Error((Exception)e.ExceptionObject);

			CompileDefinitions();
            ContainerAccessor.Resolve<IComicStore>().InitializeComicCache();

			var mainForm = new MainForm();
			//the synchronization context only becomes available after creating the form
			SynchronizationContext = SynchronizationContext.Current;	
			//the DownloadSupervisor needs the SynchronizationContext, so I resolve it only after initializing the context
			mainForm.Presenter = ContainerAccessor.Resolve<IMainPresenter>();

			ContainerAccessor.Resolve<ITrayIconController>().DisplayIcon();
            Application.Run(mainForm);
        }
    }
}
