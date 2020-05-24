using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace BookmarkManager.Models
{
    [Serializable]
    public class Configuration
    {
        private const string ConfigFileName = "config.xml";

        public string LastOpenedFile { get; set; } = "";
        public string TorBrowserPath { get; set; } = "";
        public List<string> LastOpenedFiles { get; set; } = new List<string>();
        public bool RunOnWidowsStart { get; set; }

        public WindowState MainWindowState { get; set; } = WindowState.Normal;
        public int MainWindowTop { get; set; } = 10;
        public int MainWindowLeft { get; set; } = 10;
        public int MainWindowHeight { get; set; } = 600;
        public int MainWindowWidth { get; set; } = 1000;
        


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
                MessageBox.Show(e.Message, "Config saving error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show(e.Message, "Config loading error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
    }
}
