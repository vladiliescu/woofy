using Woofy.Core;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows.ApplicationLog
{
    public class AppLogEntryAdded : IEvent
    {
        public string Message { get; private set; }
        public string Tag { get; private set; }

        public AppLogEntryAdded(string message)
            : this(null, message)
        {
        }

        public AppLogEntryAdded(string tag, string message)
        {
            Tag = tag;
            Message = message;
        }

        public override string ToString()
        {
            if (Tag.IsNotNullOrEmpty())
                return "[{0}] {1}".FormatTo(Tag, Message);
            return Message;
        }
    }
}