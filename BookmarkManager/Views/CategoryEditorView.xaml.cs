using System.Windows;
using System.Windows.Controls;

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
            if (string.IsNullOrWhiteSpace(this.DisplayNameTextBox.Text))
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

        private void DisplayNameTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {

                }
            }
        }
    }
}
