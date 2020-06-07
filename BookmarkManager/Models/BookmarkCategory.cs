using System;
using System.Collections.ObjectModel;
using BookmarkManager.MVVM;

namespace BookmarkManager.Models
{
    [Serializable]
    public class BookmarkCategory : NotificationObject
    {
        private string _displayName;
        private string _notes;
        private DateTime _timeCreated;
        
        public string DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        public string Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        public DateTime TimeCreated
        {
            get => _timeCreated;
            set => SetProperty(ref _timeCreated, value);
        }

        public ObservableCollection<Bookmark> Bookmarks { get; set; }
        public ObservableCollection<BookmarkCategory> ChildNodes { get; set; }


        public BookmarkCategory()
        {
        }

        public BookmarkCategory(string displayName, DateTime timeCreated)
        {
            _displayName = displayName;
            _timeCreated = timeCreated;
        }
    }
}
