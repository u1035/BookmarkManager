using System;
using BookmarkManager.MVVM;

namespace BookmarkManager.Models
{
    [Serializable]
    public class Bookmark : NotificationObject
    {
        #region Url property

        /// <summary>
        /// Url property
        /// </summary>
        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        private string _url;

        #endregion

        #region PageTitle property

        /// <summary>
        /// PageTitle property
        /// </summary>
        public string PageTitle
        {
            get => _pageTitle;
            set => SetProperty(ref _pageTitle, value);
        }

        private string _pageTitle;

        #endregion

        #region Notes property

        /// <summary>
        /// Notes property
        /// </summary>
        public string? Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        private string? _notes;

        #endregion

        #region Constructors

        [Obsolete("Used by serializer only")]
        public Bookmark()
        {
            _url = null!;
            _pageTitle = null!;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Bookmark"/>
        /// </summary>
        /// <param name="url"></param>
        /// <param name="pageTitle"></param>
        /// <param name="dateAdded"></param>
        /// <param name="notes"></param>
        public Bookmark(string url, string pageTitle, DateTime dateAdded, string notes)
        {
            _url = url;
            _pageTitle = pageTitle;
            _notes = notes;
        }

        #endregion
    }
}
