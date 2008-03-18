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
using Woofy.Entities;

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

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            comicsList.ItemsSource = _presenter.ActiveComicsView;
            stripsList.ItemsSource = _presenter.StripsView;
        }

        private void OnComicSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _presenter.HandleSelectedComic((Comic)comicsList.SelectedItem);
        }

        private void OnStripSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _presenter.HandleSelectedStrip((ComicStrip)stripsList.SelectedItem);
        }

        private void OnStripsMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount < 2)
                return;

            ToggleFullscreenView(true);
        }

        private void OnCurrentStripMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount < 2)
                return;

            ToggleFullscreenView(false);
        }

        private void ToggleFullscreenView(bool isFullscreen)
        {
            Visibility fullscreenVisibility = isFullscreen ? Visibility.Visible : Visibility.Collapsed;
            Visibility notFullscreenVisibility = isFullscreen ? Visibility.Collapsed : Visibility.Visible;

            currentStripPanel.Visibility = fullscreenVisibility;
            comicsList.Visibility = notFullscreenVisibility;
            stripsList.Visibility = notFullscreenVisibility;

        }
    }
}
