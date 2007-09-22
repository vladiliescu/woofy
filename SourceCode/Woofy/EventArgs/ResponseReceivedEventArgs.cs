using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Woofy.Core
{
    public class ResponseReceivedEventArgs : EventArgs
    {
        private WebResponse response;
        public WebResponse Response
        {
            get { return response; }
        }

        public ResponseReceivedEventArgs(WebResponse response)
        {
            this.response = response;
        }
    }
}
