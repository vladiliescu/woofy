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

namespace Woofy.Views
{
    public partial class SelectComics : Window
    {
        #region Instance Members
        private ComicsPresenter _controller;

        private ListCollectionView _inactiveComicsView;
        private ListCollectionView _activeComicsView; 
        #endregion

        #region Constructors
        public SelectComics(IPresenter controller)
        {
            InitializeComponent();

            _controller = (ComicsPresenter)controller;

            _inactiveComicsView = new ListCollectionView(_controller.Comics);            
            _inactiveComicsView.Filter = new Predicate<object>(delegate(object comic)
            {
                return !((Comic)comic).IsActive;
            });

            _activeComicsView = new ListCollectionView(_controller.Comics);
            _activeComicsView.Filter = new Predicate<object>(delegate(object comic)
            {
                return ((Comic)comic).IsActive;
            });

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
            _controller.ActivateComics(inactiveComicsList.SelectedItems);

            SelectListBoxItems(inactiveComicsList.SelectedItems, activeComicsList);
        }

        private void RightToLeftButton_Click(object sender, RoutedEventArgs e)
        {
            _controller.DeactivateComics(activeComicsList.SelectedItems);

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
