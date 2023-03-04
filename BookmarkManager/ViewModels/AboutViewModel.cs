using System.Reflection;

namespace BookmarkManager.ViewModels
{
    public class AboutViewModel
    {
        public string? AppName { get; }
        public string AppVersion { get; }


        /// <summary>
        /// Initializes a new instance of <see cref="AboutViewModel"/>
        /// </summary>
        public AboutViewModel()
        {
            AppName = Assembly.GetExecutingAssembly().GetName().Name;
            AppVersion = "Version " + Assembly.GetExecutingAssembly().GetName().Version;
        }
    }
}
