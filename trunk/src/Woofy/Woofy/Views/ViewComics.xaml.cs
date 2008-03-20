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
using System.Diagnostics;

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

        
        private void OnStripsMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount < 2)
                return;

            ToggleFullscreenView();
        }

        private void OnCurrentStripMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount < 2)
                return;

            ToggleFullscreenView();
        }                

        private void stripsList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                _presenter.DeleteStrips(stripsList.SelectedItems);
        }
        //TODO: e posibil sa nu fie nevoie de un intreg refresh pe strips/comics view. Cred ca e ok daca il actualizez pe firul grafic.
        private void OnMoveToLeftStrip(object sender, RoutedEventArgs e)
        {
            _presenter.MoveToPreviousStrip();            
        }

        private void OnMoveToRightStrip(object sender, RoutedEventArgs e)
        {
            _presenter.MoveToNextStrip();
        }

        #region Helpers
        private void ToggleFullscreenView()
        {
            currentStripPanel.Visibility = currentStripPanel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            comicsList.Visibility = comicsList.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            stripsList.Visibility = stripsList.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }
        #endregion

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (currentStripPanel.Visibility != Visibility.Visible)
                return;

            if (e.Delta < 0)
                _presenter.MoveToNextStrip();
            else
                _presenter.MoveToPreviousStrip();
        }
    }
}
