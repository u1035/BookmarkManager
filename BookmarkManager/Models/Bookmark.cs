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

        private string _pageTitle;
        public string PageTitle
        {
            get => _pageTitle;
            set => SetProperty(ref _pageTitle, value);
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

        public Bookmark()
        {
        }

        public Bookmark(string url, string pageTitle, DateTime dateAdded, string notes, string category)
        {
            _url = url;
            _pageTitle = pageTitle;
            _dateAdded = dateAdded;
            _notes = notes;
            _category = category;
        }
    }
}
