﻿using System;
using System.ComponentModel;
using System.Runtime.Versioning;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BookmarkManager.ViewModels;

namespace BookmarkManager.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [SupportedOSPlatform("windows")]
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (DataContext is MainViewModel model)
            {
                model.SaveCurrentBookmarkStorage();
                model.Config.SaveConfig();

                if (model.Config.CloseToTray && !model.ExitProgram) {
                    Visibility = Visibility.Hidden;
                    e.Cancel = true;
                }
            }
        }

        private void BookmarksList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBox listbox && 
                listbox.SelectedItem != null && 
                DataContext is MainViewModel mainVm) 
                mainVm.OpenInDefaultBrowser();
        }

        private void Toolbar_OnLoaded(object sender, RoutedEventArgs e)
        {
            //Hiding overflow button at the end of toolbar
            var toolBar = sender as ToolBar;
            if (toolBar?.Template.FindName("OverflowGrid", toolBar) is FrameworkElement overflowGrid)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void AnyItemTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is MainViewModel mainVm)
                mainVm.HasUnsavedChanges = true;
        }

        private void MainWindow_OnStateChanged(object? sender, EventArgs e)
        {
            if (sender is MainWindow mainWindow &&
                DataContext is MainViewModel mainVm &&
                mainWindow.WindowState == WindowState.Minimized && !mainVm.Config.ShowInTaskbar)
            {
                mainWindow.WindowState = WindowState.Normal;
                mainWindow.Visibility = Visibility.Hidden;
            }
        }
    }
}
