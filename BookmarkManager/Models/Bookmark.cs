using System;
using BookmarkManager.MVVM;

namespace BookmarkManager.Models
{
    [Serializable]
    public class Bookmark : NotificationObject
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

        private string? _notes;
        public string? Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        [Obsolete("Used by serializer only")]
        public Bookmark()
        {
            _url = null!;
            _pageTitle = null!;
        }

        public Bookmark(string url, string pageTitle, DateTime dateAdded, string notes)
        {
            _url = url;
            _pageTitle = pageTitle;
            _dateAdded = dateAdded;
            _notes = notes;
        }
    }
}
