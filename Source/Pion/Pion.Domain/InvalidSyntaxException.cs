using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pion.Domain
{
    public sealed class InvalidSyntaxException : Exception
    {
        public InvalidSyntaxException()
            : this(null)
        {
        }

        public InvalidSyntaxException(string message)
            : this(message, null)
        {
        }

        public InvalidSyntaxException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
