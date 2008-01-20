using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.Exceptions
{
    [global::System.Serializable]
    public class FileAlreadyPresentException : Exception
    {
        public FileAlreadyPresentException() { }
        public FileAlreadyPresentException(string message) : base(message) { }
        public FileAlreadyPresentException(string message, Exception inner) : base(message, inner) { }
        protected FileAlreadyPresentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
