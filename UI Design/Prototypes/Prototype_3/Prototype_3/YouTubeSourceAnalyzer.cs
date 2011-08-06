using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Prototype_3
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
            // First step: Get the Flash Variables.
            SlicableString originalSource = new SlicableString(_youTubeVideoHtmlSourceCode);

            string flashVars = originalSource.Slice("flashvars=\"", "\" ");

            // Second step: Get a list of download urls using the scent of the "videoplayback" keyword.
            var urlextractor = new UrlExtractorStrategy();

            string link = urlextractor.Extract(flashVars);

            return link;
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
