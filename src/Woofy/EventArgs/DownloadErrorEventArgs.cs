using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.Core
{
    public class DownloadErrorEventArgs : EventArgs
    {
        private Exception exception;
        public Exception Exception
        {
            get { return exception; }
        }

        private bool exceptionHandled = false;
        public bool ExceptionHandled
        {
            get { return exceptionHandled; }
            set { exceptionHandled = value; }
        }

        public DownloadErrorEventArgs(Exception exception)
        {
            this.exception = exception;
        }

    }
}
