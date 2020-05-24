using System;
using Prism.Mvvm;

namespace BookmarkManager.Models
{
    [Serializable]
    public class Bookmark : BindableBase
    {
        private string _url;
        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        private string _pageHeader;
        public string PageHeader
        {
            get => _pageHeader;
            set => SetProperty(ref _pageHeader, value);
        }

        private DateTime _dateAdded;
        public DateTime DateAdded
        {
            get => _dateAdded;
            set => SetProperty(ref _dateAdded, value);
        }

        private string _notes;
        public string Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        private string _category;
        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

    }
}
