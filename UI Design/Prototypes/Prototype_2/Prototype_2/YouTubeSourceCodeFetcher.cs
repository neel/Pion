using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Prototype_2
{
    public sealed class YouTubeSourceCodeFetcher
    {
        readonly string _youTubeVideoUrl;

        public YouTubeSourceCodeFetcher(string youTubeVideoUrl)
        {
            _youTubeVideoUrl = youTubeVideoUrl;
        }

        public string FetchHtmlSource()
        {
            WebRequest request = WebRequest.Create(_youTubeVideoUrl);
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
