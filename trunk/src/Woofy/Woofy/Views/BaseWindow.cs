using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Woofy.Controllers;
using Woofy.EventArguments;
using System.Windows.Threading;

namespace Woofy.Views
{
    public class BaseWindow : Window
    {
        protected ComicsPresenter Presenter { get; private set; }

        protected BaseWindow(ComicsPresenter presenter)
        {
            Presenter = presenter;
            Presenter.RunCodeOnUIThreadRequired += new EventHandler<RunCodeOnUIThreadRequiredEventArgs>(OnRunCodeOnUIThreadRequired);
        }

        private void OnRunCodeOnUIThreadRequired(object sender, RunCodeOnUIThreadRequiredEventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, e.Code);
        }
    }
}
