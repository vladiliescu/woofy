using Woofy.Core;
using Woofy.Core.Infrastructure;

namespace Woofy.Flows.ApplicationLog
{
    public class AppLogEntryAdded : IEvent
    {
        public string Message { get; private set; }
        public string ExpressionName { get; private set; }
        public string ComicId { get; private set; }

        public AppLogEntryAdded(string message)
            : this(message, null, null)
        {
        }

        public AppLogEntryAdded(string message, string tag, string comicId)
        {
            ExpressionName = tag;
            Message = message;
            ComicId = comicId;
        }

        public override string ToString()
        {
            if (ExpressionName.IsNotNullOrEmpty())
                return "[{0}] {1}".FormatTo(ExpressionName, Message);
            return Message;
        }
    }
}