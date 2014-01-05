using System.Text;
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
            var errorTarget = new FileTarget
            {
                Layout = "${date} ${message} ${exception:format=ToString}",
                FileName = "${basedir}/error.txt",
                Encoding = Encoding.UTF8,
                KeepFileOpen = false,
            };

            var errorRule = new LoggingRule("*", LogLevel.Error, errorTarget);
            config.AddTarget("error", errorTarget);
            config.LoggingRules.Add(errorRule);
        }
    }
}