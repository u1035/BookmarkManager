using System;
using System.Runtime.Versioning;
using Microsoft.Win32;

namespace BookmarkManager.Services
{
    [SupportedOSPlatform("windows")]
    public static class SystemStartup
    {
        private const string AppRegistryName = "BookmarkManager";
        private const string RegistryPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        public static void SetAutorunState(bool state)
        {
            if (state)
                AddRegKey();
            else
                DeleteRegKey();
        }

        private static void AddRegKey()
        {
            try
            {
                var rkApp = Registry.CurrentUser.OpenSubKey(RegistryPath, true);
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
                var rkApp = Registry.CurrentUser.OpenSubKey(RegistryPath, true);
                rkApp?.DeleteValue(AppRegistryName);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static bool GetStartupState()
        {
            var rkApp = Registry.CurrentUser.OpenSubKey(RegistryPath, true);
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
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            if (path.EndsWith("BookmarkManager.dll"))
                path = path.Replace("BookmarkManager.dll", "BookmarkManager.exe");
            return path;
        }
    }
}
