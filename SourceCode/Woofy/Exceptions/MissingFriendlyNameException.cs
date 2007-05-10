using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a <see cref="ComicInfo"/> xml structure is missing its friendlyName attribute.
    /// </summary>
    [global::System.Serializable]
    public class MissingFriendlyNameException : Exception
    {
        public MissingFriendlyNameException() { }
        public MissingFriendlyNameException(string message) : base(message) { }
        public MissingFriendlyNameException(string message, Exception inner) : base(message, inner) { }
        protected MissingFriendlyNameException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
