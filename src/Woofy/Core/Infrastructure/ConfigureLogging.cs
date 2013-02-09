using NLog;
using NLog.Config;
using NLog.Targets;

namespace Woofy.Core.Infrastructure
{
    public class ConfigureLogging
    {
        public static void Run()
        {
            var config = new LoggingConfiguration();
            
            AddErrorRule(config);

            LogManager.Configuration = config;
        }

        private static void AddErrorRule(LoggingConfiguration config)
        {
            var appInfo = ContainerAccessor.Resolve<IAppInfo>();

            var errorTarget = new EventLogTarget
                                  {
                                      Log = "Application",
                                      Source = appInfo.NameAndVersion,
                                      Layout = "${date} ${message} ${exception:format=ToString}"
                                  };
            var errorRule = new LoggingRule("*", LogLevel.Error, errorTarget);
            config.AddTarget("error", errorTarget);
            config.LoggingRules.Add(errorRule);
        }
    }
}