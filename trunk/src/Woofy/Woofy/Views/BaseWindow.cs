using System.Windows;
using Woofy.Controllers;
using Woofy.EventArguments;
using System.Windows.Threading;

namespace Woofy.Views
{
    public class BaseWindow : Window
    {
        protected ComicsPresenter Presenter { get; private set; }

        public BaseWindow()
        {
        }

        protected BaseWindow(ComicsPresenter presenter)
        {
            Presenter = presenter;
            Presenter.RunCodeOnUIThread += OnRunCodeOnUIThread;
        }

        private void OnRunCodeOnUIThread(object sender, RunCodeOnUIThreadRequiredEventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, e.Code);
        }
    }
}
