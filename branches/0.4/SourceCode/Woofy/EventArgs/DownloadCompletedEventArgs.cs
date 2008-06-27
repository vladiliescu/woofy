using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.Core
{
    public class DownloadCompletedEventArgs : EventArgs
    {
        private DownloadOutcome downloadOutcome;
        public DownloadOutcome DownloadOutcome
        {
            get { return downloadOutcome; }
        }

        public DownloadCompletedEventArgs(DownloadOutcome downloadOutcome)
        {
            this.downloadOutcome = downloadOutcome;
        }
    }
}
