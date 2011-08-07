using System.IO;
using System.Net;
using Pion.Domain;

namespace Pion.Infrastructure.Internet
{
    public sealed class WebsiteRepository : IWebsiteRepository
    {
        public WebsiteRepository()
        {
        }

        public string DownloadHtml(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
