# BookmarkManager
Standalone desktop application to store browser bookmarks

Screenshot of current developing state:
![Main window screenshot](screenshot.png)

I often making experiments with Windows, sometimes install other OS, change browsers (and use different kind of broswers, like TOR) and I don't like browser extensions for managing bookmarks. So I want to create application for saving my bookmarks in it, and opening links from it. It should have settings and bookmarks database file in application folder and be portable. Maybe it should be able to make backups. Maybe it should (in future) be cross-platform, and work both on Windows and Linux.

(Under lazy slow development)

Feel free to fork, contact me and join developing!


**To Do list**

- [ ] Page preview image or favicon
- [ ] Drag'n'Drop support
- [ ] Icon in system tray
- [ ] Nice view for bookmark item (ListBox of them?)
- [ ] Config page or window
- [ ] Help/About window
- [ ] Menu bar?
- [x] Status bar with some info - file path, bookmarks/categories count?
- [ ] Configurable path to TOR browser
- [ ] Open last used database
- [ ] List of last used databases in "File" menu
- [x] Open file specified in command line parameters
- [ ] Default action button / Enter in textbox handling - i need minimal number of actions to add bookmark
- [ ] Portability test
- [ ] Asyncronous web page details receiving - not to slow down main app thread
- [ ] Pretty UI, maybe themes, maybe dark theme
- [x] Button "Open all bookmarks" in category
- [x] Saving window position and size
- [ ] "Start with OS", "Start minimized" options
- [ ] Localization support
- [ ] Showing main window by hotkey
- [ ] Editing and deleting categories
- [ ] Editing bookmarks
- [x] Current filename in main window title
