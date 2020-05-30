using System.Reflection;

namespace BookmarkManager.ViewModels
{
    public class AboutViewModel
    {
        public string AppName { get; }
        public string AppVersion { get; }



        public AboutViewModel()
        {
            AppName = Assembly.GetExecutingAssembly().GetName().Name;
            AppVersion = "Version " + Assembly.GetExecutingAssembly().GetName().Version;
        }
    }
}
