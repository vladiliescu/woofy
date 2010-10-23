using System;

namespace Woofy.Enums
{
	[Obsolete("Should be replaced by Status")]
    public enum DownloadOutcome
    {
        None = 0,        
        NoStripMatchesRuleBroken,
        MultipleStripMatchesRuleBroken,
        Cancelled,
        Error,
        Successful
    }
}
