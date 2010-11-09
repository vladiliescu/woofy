using NLog;
using NLog.Config;
using NLog.Targets;

namespace Woofy.Core.Infrastructure
{
    public class ConfigureLogging
    {
        public static void Run()
        {
            var appInfo = ContainerAccessor.Resolve<IAppInfo>();
            var config = new LoggingConfiguration();

            var errorTarget = new EventLogTarget
            {
                Log = "Application",
                Source = appInfo.NameAndVersion,
                Layout = "${date} ${message} ${exception:format=ToString}"
            };

            var errorRule = new LoggingRule("*", LogLevel.Error, errorTarget);
            config.AddTarget("error", errorTarget);
            config.LoggingRules.Add(errorRule);

            LogManager.Configuration = config;
        }
    }
}