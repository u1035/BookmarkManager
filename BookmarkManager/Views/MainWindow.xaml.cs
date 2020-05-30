using System.ComponentModel;
using System.Diagnostics;
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
    }
}
