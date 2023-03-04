using System;
using System.Collections.ObjectModel;
using BookmarkManager.MVVM;

namespace BookmarkManager.Models
{
    [Serializable]
    public class BookmarkCategory : NotificationObject
    {
        #region DisplayName property

        /// <summary>
        /// DisplayName property
        /// </summary>
        public string DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        private string _displayName;

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

        public ObservableCollection<Bookmark> Bookmarks { get; } = new();
        public ObservableCollection<BookmarkCategory> ChildNodes { get; } = new();

        #region Constructors

        [Obsolete("Used by serializer only")]
        public BookmarkCategory()
        {
            _displayName = null!;
            _notes = null!;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BookmarkCategory"/>
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="timeCreated"></param>
        public BookmarkCategory(string displayName, DateTime timeCreated)
        {
            _displayName = displayName;
        }

        #endregion
    }
}
