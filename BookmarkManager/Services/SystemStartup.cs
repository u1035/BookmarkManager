using System;
using Microsoft.Win32;

namespace BookmarkManager.Services
{
    public static class SystemStartup
    {
        private const string AppRegistryName = "BookmarkManager";

        public static void SetAutorunState(bool state)
        {
            if (state)
            {
                AddRegKey();
            }
            else
            {
                DeleteRegKey();
            }
        }

        private static void AddRegKey()
        {
            try
            {
                var rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                rkApp?.SetValue(AppRegistryName, GetStartupPath());
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private static void DeleteRegKey()
        {
            try
            {
                var rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                rkApp?.DeleteValue(AppRegistryName);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static bool GetStartupState()
        {
            var rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (rkApp != null)
            {
                var value = rkApp.GetValue(AppRegistryName);
                var startupPath = GetStartupPath();
                if (value != null && value.ToString() == startupPath)
                {
                    return true;
                }
            }

            return false;
        }

        private static string GetStartupPath()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().Location;
        }
    }
}
