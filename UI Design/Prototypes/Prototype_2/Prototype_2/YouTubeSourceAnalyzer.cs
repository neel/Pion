using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Prototype_2
{
    public sealed class YouTubeSourceAnalyzer
    {
        readonly string _youTubeVideoHtmlSourceCode;

        public YouTubeSourceAnalyzer(string youTubeVideoHtmlSourceCode)
        {
            _youTubeVideoHtmlSourceCode = youTubeVideoHtmlSourceCode;
        }

        public string ExtractDirectDownloadLink()
        {
            try
            {
                string startTag = "url_encoded_fmt_stream_map=";
                string endTag = "&amp;";

                SlicableString slicey = new SlicableString(_youTubeVideoHtmlSourceCode);
                string fmt_stream_map = slicey.Slice(startTag, endTag);
                string unescapedMap = Uri.UnescapeDataString(fmt_stream_map);
                string firstLink = Uri.UnescapeDataString(unescapedMap.Substring(4, unescapedMap.IndexOf("&itag=") - 4));

                return firstLink;
            }
            catch
            {
                return string.Empty;
            }
        }

        public string ExtractTitle()
        {
            string startTag = "<span id=\"eow-title\" class=\"\" dir=\"ltr\" title=\"";
            string endTag = "\">";

            SlicableString slicey = new SlicableString(_youTubeVideoHtmlSourceCode);

            return slicey.Slice(startTag, endTag);
        }
    }
}
