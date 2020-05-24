using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using BookmarkManager.Models;
using BookmarkManager.Services;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;

namespace BookmarkManager.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private bool _bookmarkStorageLoaded;
        public bool BookmarkStorageLoaded
        {
            get => _bookmarkStorageLoaded;
            set => SetProperty(ref _bookmarkStorageLoaded, value);
        }

        private bool _categorySelected;
        public bool CategorySelected
        {
            get => _categorySelected;
            set => SetProperty(ref _categorySelected, value);
        }


        private string _categoryText;
        public string CategoryText
        {
            get => _categoryText;
            set => SetProperty(ref _categoryText, value);
        }

        private string _urlText;
        public string UrlText
        {
            get => _urlText;
            set => SetProperty(ref _urlText, value);
        }

        private string _selectedCategory;
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (SetProperty(ref _selectedCategory, value))
                {
                    RefreshCategory();
                }
            }
        }

        private Bookmark _selectedBookmark;
        public Bookmark SelectedBookmark
        {
            get => _selectedBookmark;
            set
            {
                if (SetProperty(ref _selectedBookmark, value))
                {
                    if (value != null)
                    {

                    }
                }
            }
        }

        private BookmarkStorage _currentBookmarkStorage;
        public BookmarkStorage CurrentBookmarkStorage
        {
            get => _currentBookmarkStorage;
            set
            {
                if (SetProperty(ref _currentBookmarkStorage, value))
                {
                    BookmarkStorageLoaded = (value != null);
                    DisplayingBookmarks.Clear();
                }
            }
        }

        private string _currentFileName;
        public string CurrentFileName
        {
            get => _currentFileName;
            set => SetProperty(ref _currentFileName, value);
        }

        private bool _bookmarkStorageModified;

        public ObservableCollection<Bookmark> DisplayingBookmarks { get; set; } = new ObservableCollection<Bookmark>();
        public Configuration Config { get; set; }

        public DelegateCommand AddCategoryCommand { get; }
        public DelegateCommand AddLinkCommand { get; }
        public DelegateCommand NewDbCommand { get; }
        public DelegateCommand OpenDbCommand { get; }
        public DelegateCommand SaveDbCommand { get; }
        public DelegateCommand OpenInDefaultBrowserCommand { get; }
        public DelegateCommand OpenAllCommand { get; }
        public DelegateCommand DeleteBookmarkCommand { get; }


        public MainViewModel()
        {
            AddCategoryCommand = new DelegateCommand(AddCategory);
            AddLinkCommand = new DelegateCommand(AddLink);
            NewDbCommand = new DelegateCommand(NewDb);
            OpenDbCommand = new DelegateCommand(OpenDb);
            SaveDbCommand = new DelegateCommand(SaveDb);
            OpenInDefaultBrowserCommand = new DelegateCommand(OpenInDefaultBrowser);
            OpenAllCommand = new DelegateCommand(OpenAll);
            DeleteBookmarkCommand= new DelegateCommand(DeleteBookmark);

            Config = Configuration.LoadFromFile();

            CheckCommandLineArgs();
        }

        private void OpenAll()
        {
            if (SelectedCategory == null) return;
            foreach (var bookmark in DisplayingBookmarks)
            {
                System.Diagnostics.Process.Start(bookmark.Url);
            }
        }

        private void DeleteBookmark()
        {
            if (SelectedBookmark == null) return;
            CurrentBookmarkStorage.Bookmarks.Remove(SelectedBookmark);

            RefreshCategory();
            SaveCurrentBookmarkStorage();
        }

        private void OpenInDefaultBrowser()
        {
            if (SelectedBookmark == null) return;
            System.Diagnostics.Process.Start(SelectedBookmark.Url);
        }

        private void CheckCommandLineArgs()
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length <= 1) return;

            if (File.Exists(args[1]))
            {
                OpenBookmarkDb(args[1]);
            }
        }

        private void NewDb()
        {
            //todo: check unsaved changes

            _currentFileName = "";
            CurrentBookmarkStorage = new BookmarkStorage();
            _bookmarkStorageModified = true;

            SaveCurrentBookmarkStorage("Choose filename for new database");
        }

        private void OpenDb()
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Bookmarks DB file (*.xml)|*.xml",
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
            };
            if (openFileDialog.ShowDialog() == true)
            {
                OpenBookmarkDb(openFileDialog.FileName);
            }
        }

        private void OpenBookmarkDb(string fileName)
        {
            _currentFileName = fileName;
            CurrentBookmarkStorage = BookmarkStorage.LoadFromFile(_currentFileName);
            if (CurrentBookmarkStorage.Categories.Count > 0)
            {
                SelectedCategory = CurrentBookmarkStorage.Categories[0];
            }
        }

        private void SaveDb()
        {
            SaveCurrentBookmarkStorage();
        }

        private void SaveCurrentBookmarkStorage(string header = "")
        {
            if (string.IsNullOrEmpty(_currentFileName))
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Bookmarks DB file (*.xml)|*.xml",
                    InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
                };
                if (header != "") saveFileDialog.Title = header;

                if (saveFileDialog.ShowDialog() == true)
                {
                    _currentFileName = saveFileDialog.FileName;
                    CurrentBookmarkStorage.SaveStorage(_currentFileName);
                    _bookmarkStorageModified = false;
                }
            }
            else
            {
                CurrentBookmarkStorage.SaveStorage(_currentFileName);
                _bookmarkStorageModified = false;
            }
        }

        private void AddLink()
        {
            var title = WebPageParser.GetPageTitle(UrlText);
            CurrentBookmarkStorage.Bookmarks.Add(new Bookmark(UrlText, title, DateTime.Now, "", SelectedCategory));
            RefreshCategory();
            SaveCurrentBookmarkStorage();
        }

        private void AddCategory()
        {
            CurrentBookmarkStorage.Categories.Add(CategoryText);
            SaveCurrentBookmarkStorage();
        }

        private void RefreshCategory()
        {
            if (_selectedCategory != null)
            {
                //todo: find more elegant way
                DisplayingBookmarks = new ObservableCollection<Bookmark>(CurrentBookmarkStorage.Bookmarks.Where(b => b.Category == _selectedCategory));
                RaisePropertyChanged("DisplayingBookmarks");
                CategorySelected = true;
            }
            else
            {
                CategorySelected = false;
            }
        }
    }
}
