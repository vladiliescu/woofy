using System;

using log4net;
using log4net.Config;

namespace Woofy.Core
{
    /// <summary>
    /// Handles logging
    /// </summary>
    public class Logger
    {
        private static readonly ILog logger;

        /// <summary>
        /// Initializes the internal logger.
        /// </summary>
        static Logger()
        {
            XmlConfigurator.Configure();
            logger = LogManager.GetLogger(typeof(Logger));
        }

        /// <summary>
        /// Logs an informative message.
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void LogInformation(string message)
        {
            logger.Info(message);
        }

        /// <summary>
        /// Logs a formatted message.
        /// </summary>
        /// <param name="messageFormat">The message format.</param>
        /// <param name="args">Arguments for formatting the message.</param>
        public static void LogInformation(string messageFormat, params object[] args)
        {
            logger.InfoFormat(messageFormat, args);
        }

        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        public static void LogException(Exception ex)
        {
            logger.Error("An exception has occured.", ex);
        }

        /// <summary>
        /// Logs an exception, and a message regarding it
        /// </summary>
        /// <param name="message">The message regarding the exception.</param>
        /// <param name="ex">The exception to log.</param>
        public static void LogException(string message, Exception ex)
        {
            logger.Error(message, ex);
        }
    }
}
