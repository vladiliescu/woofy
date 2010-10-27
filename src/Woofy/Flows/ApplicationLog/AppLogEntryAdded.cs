namespace Woofy.Flows.ApplicationLog
{
    public class AppLogEntryAdded
    {
        public string Message { get; private set; }

        public AppLogEntryAdded(string message)
        {
            Message = message;
        }
    }
}