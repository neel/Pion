using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype_3
{
    public sealed class StringMarkingNotFoundException : Exception
    {
        readonly string _marking;

        public StringMarkingNotFoundException(string marking)
            : this(marking, null)
        {
        }

        public StringMarkingNotFoundException(string marking, string message)
            : this(marking, message, null)
        {
        }

        public StringMarkingNotFoundException(string marking, string message, Exception innerException)
            : base(message, innerException)
        {
            _marking = marking;
        }

        public string Marking
        {
            get
            {
                return _marking;
            }
        }
    }
}
