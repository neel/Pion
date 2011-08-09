using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pion.Domain
{
    public sealed class SlicableString
    {
        readonly string _value;

        public SlicableString(string value)
        {
            _value = value;
        }

        public string Slice(string beginningMark, string endingMark)
        {
            int sliceStartPosition = _value.IndexOf(beginningMark) + beginningMark.Length;
            int sliceEndPosition = _value.IndexOf(endingMark, sliceStartPosition);

            return _value.Substring(sliceStartPosition, sliceEndPosition - sliceStartPosition);
        }
    }
}
