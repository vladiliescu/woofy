using Woofy.Core.Infrastructure;
using Woofy.Core;

namespace Woofy.Flows.ApplicationLog
{
    public interface IAppLog
    {
        void Send(string message);
        void Send(string messageFormat, params object[] args);
        void Send(AppLogEntryAdded logEntry);
    }

    public class AppLog : IAppLog
    {
        private readonly IAppController appController;

        public AppLog(IAppController appController)
        {
            this.appController = appController;
        }

        public void Send(string message)
        {
            appController.Raise(new AppLogEntryAdded(message));
        }

        public void Send(string messageFormat, params object[] args)
        {
            Send(messageFormat.FormatTo(args));
        }

        public void Send(AppLogEntryAdded logEntry)
        {
            appController.Raise(logEntry);
        }
    }
}