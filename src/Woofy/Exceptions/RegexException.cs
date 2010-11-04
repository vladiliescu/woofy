using System;
using System.Runtime.Serialization;

namespace Woofy.Exceptions
{
    [Serializable]
	[Obsolete]
    public class RegexException : Exception
    {
        public RegexException()
        {
        }

        public RegexException(string message) : base(message)
        {
        }

        public RegexException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RegexException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}