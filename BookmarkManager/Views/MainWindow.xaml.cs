using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
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
            InitializeComponent();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            ((MainViewModel)DataContext).SaveCurrentBookmarkStorage();
            ((MainViewModel)DataContext).Config.SaveConfig();
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
    }
}
