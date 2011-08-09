using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Web;

namespace Pion.Domain
{
    public sealed class YouTubeParser
    {
        readonly string _htmlSource;

        const int VALUE_NOT_FOUND = -1;

        internal const string VIDEO_START_TAG = "<span id=\"eow-title\" class=\"\" dir=\"ltr\" title=\"";
        internal const string VIDEO_END_TAG = "\">";

        public YouTubeParser(string htmlSource)
        {
            if (string.IsNullOrWhiteSpace(htmlSource))
            {
                throw new ArgumentNullException("htmlSource");
            }

            _htmlSource = htmlSource;
        }

        public string ExtractTitle()
        {
            string videoTitle = ExtractSlice(_htmlSource, VIDEO_START_TAG, VIDEO_END_TAG);

            if (string.IsNullOrWhiteSpace(videoTitle))
            {
                ThrowInvalidSyntaxException();
            }

            return videoTitle;
        }

        public string ExtractDownloadLink()
        {
            // First step: Get the Flash Variables.
            SlicableString originalSource = new SlicableString(_htmlSource);

            string flashVars = originalSource.Slice("flashvars=\"", "\" ");

            DissectibleString steeringWheel = new DissectibleString(flashVars);

            List<string> urls = new List<string>();

            while (true)
            {
                try
                {
                    steeringWheel.MoveForwardTo("videoplayback");
                    steeringWheel.MoveBackwardsTo("url%3D");
                    steeringWheel.SetStartMarker();

                    try
                    {
                        steeringWheel.MoveForwardTo("%2Curl%3D");
                    }
                    catch (StringMarkingNotFoundException)
                    {
                        steeringWheel.MoveForwardTo("&amp");
                    }

                    steeringWheel.SetEndMarker();

                    string firstURl = steeringWheel.GetMarkedRange();

                    string unescapedUrl = Uri.UnescapeDataString(firstURl);

                    Regex itagRegex = new Regex(@"&?itag=\d+", RegexOptions.IgnoreCase);

                    string usableUrl = itagRegex.Replace(unescapedUrl, string.Empty);

                    NameValueCollection ho = HttpUtility.ParseQueryString(usableUrl);

                    urls.Add(Uri.UnescapeDataString(usableUrl));
                }
                catch (StringMarkingNotFoundException)
                {
                    break;
                }
            }

            return urls.Where(value => value.Contains("flv")).FirstOrDefault();
        }

        string ExtractSlice(string value, string beginningMark, string endingMark)
        {
            if (value.Length <= beginningMark.Length)
            {
                ThrowInvalidSyntaxException();
            }

            int sliceStartPosition = value.IndexOf(beginningMark) + beginningMark.Length;

            if (sliceStartPosition == VALUE_NOT_FOUND)
            {
                ThrowInvalidSyntaxException();
            }

            int sliceEndPosition = value.IndexOf(endingMark, sliceStartPosition);

            if (sliceEndPosition == VALUE_NOT_FOUND)
            {
                ThrowInvalidSyntaxException();
            }

            if (sliceStartPosition > sliceEndPosition)
            {
                ThrowInvalidSyntaxException();
            }

            return value.Substring(sliceStartPosition, sliceEndPosition - sliceStartPosition);
        }

        void ThrowInvalidSyntaxException()
        {
            throw new InvalidSyntaxException();
        }
    }
}
