using System.Windows;

namespace BookmarkManager.Views
{
    /// <summary>
    /// Interaction logic for BookmarkEditorView.xaml
    /// </summary>
    public partial class BookmarkEditorView
    {
        public BookmarkEditorView()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PageTitleTextBox.Text))
            {
                MessageBox.Show(Properties.Resources.BookmarkEmptyMessageBox, Properties.Resources.BookmarkEditWindowTitle, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (string.IsNullOrWhiteSpace(UrlTextBox.Text))
            {
                MessageBox.Show(Properties.Resources.BookmarkEmptyUrlMessageBox, Properties.Resources.BookmarkEditWindowTitle, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            DialogResult = true;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
