using Woofy.Core.Infrastructure;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine.Methods
{
    public static class Utils
    {
        public static void Log(string message)
        {
            ContainerAccessor.Resolve<IAppLog>().Send(new AppLogEntryAdded(message));
        }
    }
}