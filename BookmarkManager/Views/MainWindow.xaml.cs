using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BookmarkManager.ViewModels;

namespace BookmarkManager.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            //System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
            InitializeComponent();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            ((MainViewModel)DataContext).SaveCurrentBookmarkStorage();
            ((MainViewModel)DataContext).Config.SaveConfig();

            if (((MainViewModel) DataContext).Config.CloseToTray && !((MainViewModel) DataContext).ExitProgram)
            {
                this.Visibility = Visibility.Hidden;
                e.Cancel = true;
            }
        }

        private void BookmarksList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBox listbox)
                if (listbox.SelectedItem != null)
                    if (this.DataContext is MainViewModel mainVm)
                        mainVm.OpenInDefaultBrowser();
        }

        private void Toolbar_OnLoaded(object sender, RoutedEventArgs e)
        {
            //Hiding overflow button at the end of toolbar
            var toolBar = sender as ToolBar;
            if (toolBar?.Template.FindName("OverflowGrid", toolBar) is FrameworkElement overflowGrid)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void AnyItemTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.DataContext is MainViewModel mainVm)
                mainVm.HasUnsavedChanges = true;
        }

        private void MainWindow_OnStateChanged(object? sender, EventArgs e)
        {
            if (sender is MainWindow mainWindow)
            {
                if (this.DataContext is MainViewModel mainVm)
                {
                    if (mainWindow.WindowState == WindowState.Minimized && !mainVm.Config.ShowInTaskbar)
                    {
                        mainWindow.WindowState = WindowState.Normal;
                        mainWindow.Visibility = Visibility.Hidden;
                    }
                }
            }
        }
    }
}
