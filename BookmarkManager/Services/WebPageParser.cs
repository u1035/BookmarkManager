using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace BookmarkManager.Services
{
    public static class WebPageParser
    {
        public static string GetPageTitle(string url)
        {
            var title = "";
            try
            {
                var webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                var html = webClient.DownloadString(url);

                title = Regex.Match(html, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
                    RegexOptions.IgnoreCase).Groups["Title"].Value;

                
            }
            catch (Exception e)
            {
                return title;
            }
            return title;
        }
    }
}
