using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pion.Domain
{
    public sealed class YouTubeParser
    {
        readonly string _htmlSource;

        const int VALUE_NOT_FOUND = -1;

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
            string startTag = "<span id=\"eow-title\" class=\"\" dir=\"ltr\" title=\"";
            string endTag = "\">";

            string videoTitle = ExtractSlice(_htmlSource, startTag, endTag);

            if (string.IsNullOrWhiteSpace(videoTitle))
            {
                ThrowInvalidSyntaxException();
            }

            return videoTitle;
        }

        public string ExtractDownloadLink()
        {
            return string.Empty;
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
