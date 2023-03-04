using System.Windows;

namespace BookmarkManager.Views
{
    /// <summary>
    /// Interaction logic for CategoryEditorView.xaml
    /// </summary>
    public partial class CategoryEditorView
    {
        public CategoryEditorView()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DisplayNameTextBox.Text))
            {
                MessageBox.Show(Properties.Resources.CategoryEmptyMessageBox, Properties.Resources.CategoryEditWindowTitle, MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
