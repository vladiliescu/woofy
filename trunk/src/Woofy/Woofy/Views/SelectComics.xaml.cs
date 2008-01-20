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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

using Woofy.Controllers;
using Woofy.Entities;
using System.Windows.Threading;

namespace Woofy.Views
{
    public partial class SelectComics : Window
    {
        #region Instance Members
        private ComicsPresenter _presenter;

        private ListCollectionView _inactiveComicsView;
        private ListCollectionView _activeComicsView; 
        #endregion

        #region Constructors
        public SelectComics(IPresenter presenter)
        {
            InitializeComponent();

            _presenter = (ComicsPresenter)presenter;
            _presenter.RefreshViewsRequired += new EventHandler(Presenter_RefreshViewsRequired);

            _inactiveComicsView = new ListCollectionView(_presenter.Comics);            
            _inactiveComicsView.Filter = new Predicate<object>(delegate(object comic)
            {
                return !((Comic)comic).IsActive;
            });

            _activeComicsView = new ListCollectionView(_presenter.Comics);
            _activeComicsView.Filter = new Predicate<object>(delegate(object comic)
            {
                return ((Comic)comic).IsActive;
            });

        }

        private delegate void MethodInvoker();

        private void Presenter_RefreshViewsRequired(object sender, EventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new MethodInvoker(delegate{
                _inactiveComicsView.Refresh();
                _activeComicsView.Refresh();    
            }));            
        } 
        #endregion

        #region Events - Window
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            inactiveComicsList.ItemsSource = _inactiveComicsView;
            activeComicsList.ItemsSource = _activeComicsView;
        } 
        #endregion

        #region Events - Buttons
        private void LeftToRightButton_Click(object sender, RoutedEventArgs e)
        {
            _presenter.ActivateComics(inactiveComicsList.SelectedItems);

            SelectListBoxItems(inactiveComicsList.SelectedItems, activeComicsList);
        }

        private void RightToLeftButton_Click(object sender, RoutedEventArgs e)
        {
            _presenter.DeactivateComics(activeComicsList.SelectedItems);

            SelectListBoxItems(activeComicsList.SelectedItems, inactiveComicsList);
        } 
        #endregion

        #region Methods
        private void SelectListBoxItems(IList items, ListBox listBox)
        {
            //avoid having other comics selected
            listBox.SelectedIndex = -1;
            foreach (object item in items)
                listBox.SelectedItems.Add(item);

            _inactiveComicsView.Refresh();
            _activeComicsView.Refresh();
        }
        #endregion
    }
}
