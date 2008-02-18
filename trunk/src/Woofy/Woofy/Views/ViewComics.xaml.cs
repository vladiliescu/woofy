using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Woofy.Controllers;
using Woofy.EventArguments;
using System.Windows.Threading;

namespace Woofy.Views
{
    public partial class ViewComics : Window
    {
        private ComicsPresenter _presenter;

        public ViewComics(ComicsPresenter presenter)
        {
            InitializeComponent();

            _presenter = presenter;
            _presenter.RunCodeOnUIThreadRequired += new EventHandler<RunCodeOnUIThreadRequiredEventArgs>(OnRunCodeOnUIThreadRequired);
        }

        private void OnRunCodeOnUIThreadRequired(object sender, RunCodeOnUIThreadRequiredEventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, e.Code);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            comicsList.ItemsSource = _presenter.ActiveComicsView;
        } 
    }
}
