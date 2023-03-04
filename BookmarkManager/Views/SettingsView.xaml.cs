using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace BookmarkManager.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView
    {
        public SettingsView()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void TorBrowse_OnClick(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog()
            {
                DefaultExt=".exe",
                Filter = "Executable files (*.exe)|*.exe",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            if (ofd.ShowDialog() == true)
            {
                TorPathTextBox.Text = ofd.FileName;
                TorPathTextBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            }
        }
    }
}
