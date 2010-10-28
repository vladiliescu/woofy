using System;
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
        private readonly IApplicationController applicationController;

        public AppLog(IApplicationController applicationController)
        {
            this.applicationController = applicationController;
        }

        public void Send(string message)
        {
            applicationController.Raise(new AppLogEntryAdded(message));
        }

        public void Send(string messageFormat, params object[] args)
        {
            Send(messageFormat.FormatTo(args));
        }

        public void Send(AppLogEntryAdded logEntry)
        {
            applicationController.Raise(logEntry);
        }
    }
}