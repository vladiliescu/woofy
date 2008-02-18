using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.EventArguments
{
    public class RunCodeOnUIThreadRequiredEventArgs : EventArgs
    {
        public Delegate Code { get; private set; }

        public RunCodeOnUIThreadRequiredEventArgs(Delegate code)
        {
            Code = code;
        }
    }
}
