using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace BookmarkManager.Services
{
    public static class WebPageParser
    {
        public static string GetPageTitle(string url)
        {
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            var html = webClient.DownloadString(url);

            var title = Regex.Match(html, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
                RegexOptions.IgnoreCase).Groups["Title"].Value;

            return title;
        }
    }
}
