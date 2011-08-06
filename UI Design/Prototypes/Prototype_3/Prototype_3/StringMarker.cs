using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype_3
{
    public sealed class StringMarker
    {
        readonly string _value;
        readonly int _position;

        public StringMarker(int position, string value)
        {
            _position = position;
            _value = value;
        }

        public int Length
        {
            get
            {
                return _value.Length;
            }
        }

        public int Position
        {
            get
            {
                return _position;
            }
        }

        public string Value
        {
            get
            {
                return _value;
            }
        }
    }
}
