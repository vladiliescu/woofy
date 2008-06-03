using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Woofy.Controllers;
using Woofy.Entities;
using Woofy.Other;

namespace Woofy.Views
{
    public partial class ViewComics
    {       

        public ViewComics(ComicsPresenter presenter)
            : base (presenter)
        {
            InitializeComponent();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            comicsList.ItemsSource = Presenter.ActiveAndSortedComicsView;            
            stripsList.ItemsSource = Presenter.StripsView;

            RegisterKeyBindings();

            MoveFocusTo(FocusNavigationDirection.Next);
        }        

        private void OnComicSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Presenter.HandleSelectedComic((Comic)comicsList.SelectedItem);
        }

        private void OnStripSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Presenter.HandleSelectedStrip((ComicStrip)stripsList.SelectedItem);
        }

        private void OnStripsMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount < 2)
                return;

            ToggleDetailsView();
        }

        private void OnCurrentStripMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount < 2)
                return;

            ToggleDetailsView();
        }

        private void ToggleDetailsView()
        {
            bool hideDetailsView = currentStripPanel.Visibility == Visibility.Visible;

            if (hideDetailsView)
            {
                currentStripPanel.Visibility = Visibility.Collapsed;
                comicsList.Visibility = Visibility.Visible;
                stripsList.Visibility = Visibility.Visible;

                MoveFocusTo(FocusNavigationDirection.Previous);

                return;
            }

            currentStripPanel.Visibility = Visibility.Visible;
            comicsList.Visibility = Visibility.Collapsed;
            stripsList.Visibility = Visibility.Collapsed;
        }

        private bool IsKeyboardFocusWithinStripsList()
        {
            return stripsList.IsKeyboardFocusWithin;
        }

        private bool IsKeyboardFocusWithinComicsList()
        {
            return comicsList.IsKeyboardFocusWithin;
        }

        private bool IsCurrentStripPanelVisible()
        {
            return (currentStripPanel.Visibility == Visibility.Visible);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            KeyboardManager.CheckKeyBindings(e.Key);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Presenter.MoveToPreviousStrip();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Presenter.MoveToNextStrip();
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                Presenter.MoveToPreviousStrip();
            else
                Presenter.MoveToNextStrip();
        }

        private static void MoveFocusTo(FocusNavigationDirection direction)
        {
            var focusedElement = Keyboard.FocusedElement as UIElement;
            if (focusedElement == null)
                return;

            focusedElement.MoveFocus(new TraversalRequest(direction));
        }

        private static void MoveFocusToNextElement()
        {
            MoveFocusTo(FocusNavigationDirection.Next);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (currentStripPanel.Visibility == Visibility.Visible)
            {
                ToggleDetailsView();

                e.Cancel = true;
                return;
            }
        }

        private void RegisterKeyBindings()
        {
            KeyboardManager.RegisterKeyBinding(
                IsKeyboardFocusWithinStripsList,
                ToggleDetailsView,
                Key.Enter
                );

            KeyboardManager.RegisterKeyBinding(
                IsCurrentStripPanelVisible,
                ToggleDetailsView,
                Key.Enter, Key.Escape
                );

            KeyboardManager.RegisterKeyBinding(
                IsCurrentStripPanelVisible,
                Presenter.MoveToPreviousStrip,
                Key.Left, Key.PageUp, Key.Back
                );

            KeyboardManager.RegisterKeyBinding(
                IsCurrentStripPanelVisible,
                Presenter.MoveToNextStrip,
                Key.Right, Key.PageDown, Key.Space
                );

            KeyboardManager.RegisterKeyBinding(
                IsCurrentStripPanelVisible,
                Presenter.MoveToFirstStrip,
                Key.Home
                );

            KeyboardManager.RegisterKeyBinding(
                IsCurrentStripPanelVisible,
                Presenter.MoveToLastStrip,
                Key.End
                );

            KeyboardManager.RegisterKeyBinding(
                IsCurrentStripPanelVisible,
                DisplayCurrentStripInScaledSize,
                Key.Multiply
                );

            KeyboardManager.RegisterKeyBinding(
                IsCurrentStripPanelVisible,
                DisplayCurrentStripInActualSize,
                Key.Divide
                );

            KeyboardManager.RegisterKeyBinding(
                IsKeyboardFocusWithinComicsList,
                MoveFocusToNextElement,
                Key.Enter
                );

            KeyboardManager.RegisterKeyBinding(
                IsKeyboardFocusWithinStripsList,
                DeleteSelectedStrips,
                Key.Delete
                );
        }

        private void DeleteSelectedStrips()
        {
            Presenter.DeleteStrips(stripsList.SelectedItems);
        }

        private void DisplayCurrentStripInActualSize()
        {
            currentStrip.Stretch = Stretch.None;
        }

        private void DisplayCurrentStripInScaledSize()
        {
            currentStrip.Stretch = Stretch.Uniform;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Presenter.MoveComicUp((Comic)comicsList.SelectedItem);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Presenter.MoveComicDown((Comic)comicsList.SelectedItem);
        }
    }
}
