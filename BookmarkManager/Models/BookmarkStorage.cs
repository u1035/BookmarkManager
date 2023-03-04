using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using BookmarkManager.MVVM;

namespace BookmarkManager.Models
{
    [Serializable]
    public class BookmarkStorage : NotificationObject
    {
        public ObservableCollection<BookmarkCategory> Categories { get; } = new();

        public int SelectedCategoryIndex { get; set; }

        public int GetTotalBookmarksCount() => GetNodeRecordsCount(Categories);

        private int GetNodeRecordsCount(IEnumerable<BookmarkCategory> categories)
        {
            var result = 0;
            foreach (var category in categories)
            {
                result += category.Bookmarks.Count;
                result += GetNodeRecordsCount(category.ChildNodes);
            }

            return result;
        }


        #region Saving/Loading

        public void SaveStorage(string fileName)
        {
            var formatter = new XmlSerializer(typeof(BookmarkStorage));
            try
            {
                using var fs = new FileStream(fileName, FileMode.Create);
                formatter.Serialize(fs, this);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Properties.Resources.FileSavingError, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static BookmarkStorage? LoadFromFile(string fileName)
        {
            if (!File.Exists(fileName)) return null;

            try
            {
                var formatter = new XmlSerializer(typeof(BookmarkStorage));
                using var fs = new FileStream(fileName, FileMode.Open);
                return formatter.Deserialize(fs) as BookmarkStorage;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Properties.Resources.FileLoadingError, MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        #endregion
    }
}
