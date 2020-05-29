using System.ComponentModel;
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
    }
}
