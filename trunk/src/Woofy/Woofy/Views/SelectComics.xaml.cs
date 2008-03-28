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
using Woofy.EventArguments;

namespace Woofy.Views
{
    public partial class SelectComics : BaseWindow
    {
        #region Instance Members
        private ComicsPresenter _presenter;
        #endregion

        #region Constructors
        public SelectComics(ComicsPresenter presenter)
            : base(presenter)
        {
            InitializeComponent();
        }        
        #endregion

        #region Events - Window
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            inactiveComicsList.ItemsSource = Presenter.InactiveComicsView;
            activeComicsList.ItemsSource = Presenter.ActiveComicsView;
        } 
        #endregion

        #region Events - Buttons
        private void LeftToRightButton_Click(object sender, RoutedEventArgs e)
        {
            Presenter.ActivateComics(inactiveComicsList.SelectedItems);

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

            Presenter.ActiveComicsView.Refresh();
            Presenter.InactiveComicsView.Refresh();
        }
        #endregion
    }
}
