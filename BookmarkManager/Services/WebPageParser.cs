using System;
using System.Net;
using System.Text.RegularExpressions;

namespace BookmarkManager.Services
{
    public static class WebPageParser
    {
        //todo: replace with HttpClient, because WebRequest is deprecated in NET
        public static string GetPageTitle(string url)
        {
            var title = "";
            try
            {
                var request = WebRequest.Create(url);
                var response = request.GetResponse();

                var webClient = new WebClient();
                webClient.Headers.Add(HttpRequestHeader.ContentType, response.ContentType);
                var html = webClient.DownloadString(url);

                title = Regex.Match(html, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
                    RegexOptions.IgnoreCase).Groups["Title"].Value;
                
            }
            catch (Exception)
            {
                return title;
            }
            return title;
        }
    }
}
