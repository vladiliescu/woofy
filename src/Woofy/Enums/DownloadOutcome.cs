namespace Woofy.Enums
{
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
