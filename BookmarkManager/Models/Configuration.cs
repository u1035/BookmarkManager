using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;
using BookmarkManager.MVVM;

namespace BookmarkManager.Models
{
    [Serializable]
    public class Configuration : NotificationObject
    {
        private bool _showInTaskbar = true;
        private const string ConfigFileName = "config.xml";

        public string LastOpenedFile { get; set; } = "";
        public string TorBrowserPath { get; set; } = "";
        public ObservableCollection<string> LastOpenedFiles { get; set; } = new ObservableCollection<string>();
        public bool RunOnWidowsStart { get; set; }

        public bool ShowInTaskbar
        {
            get => _showInTaskbar;
            set => SetProperty(ref _showInTaskbar, value);
        }

        public bool StartMinimized { get; set; } = true;
        public bool OpenLastUsedFile { get; set; }
        public bool CloseToTray { get; set; } = true;

        public WindowState MainWindowState { get; set; } = WindowState.Normal;
        public int MainWindowTop { get; set; } = 10;
        public int MainWindowLeft { get; set; } = 10;
        public int MainWindowHeight { get; set; } = 600;
        public int MainWindowWidth { get; set; } = 1000;

        [XmlIgnore]
        public Command ClearRecentFilesCommand { get; }

        public Configuration()
        {
            ClearRecentFilesCommand = new Command(ClearRecentFiles);
        }

        private void ClearRecentFiles()
        {
            LastOpenedFiles?.Clear();
        }


        public void SaveConfig()
        {
            var formatter = new XmlSerializer(typeof(Configuration));
            try
            {
                using (var fs = new FileStream(ConfigFileName, FileMode.Create))
                {
                    formatter.Serialize(fs, this);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Properties.Resources.ConfigSavingError, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static Configuration LoadFromFile()
        {
            if (!File.Exists(ConfigFileName)) return new Configuration();

            try
            {
                var formatter = new XmlSerializer(typeof(Configuration));
                using (var fs = new FileStream(ConfigFileName, FileMode.Open))
                {
                    return ((Configuration)formatter.Deserialize(fs));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Properties.Resources.ConfigLoadingError, MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public void AddRecentFile(string fileName)
        {
            if (LastOpenedFiles.Contains(fileName))
                LastOpenedFiles.Remove(fileName);

            LastOpenedFiles.Insert(0, fileName);

            while (LastOpenedFiles.Count > 5)
            {
                LastOpenedFiles.Remove(LastOpenedFiles.Last());
            }
        }
    }
}
