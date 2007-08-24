using System;

using log4net;
using log4net.Core;
using log4net.Config;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using System.IO;
using System.Text;

namespace Woofy.Core
{
    /// <summary>
    /// Handles logging
    /// </summary>
    public class Logger
    {
        private static readonly ILog logger;
        private static readonly ILog debugLogger;
        private static readonly MemoryAppender memoryAppender;
        private static readonly IAppender fileAppender;

        /// <summary>
        /// Initializes the internal logger.
        /// </summary>
        static Logger()
        {
            XmlConfigurator.Configure();
            logger = LogManager.GetLogger(typeof(Logger));

            debugLogger = LogManager.GetLogger("Debug Logger");
            memoryAppender = new MemoryAppender();
            memoryAppender.Name = "MemoryAppender";
            memoryAppender.ActivateOptions();

            fileAppender = ((log4net.Repository.Hierarchy.Logger)debugLogger.Logger).Parent.GetAppender("appender");
            ((log4net.Repository.Hierarchy.Logger)debugLogger.Logger).AddAppender(memoryAppender);
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

        private static object debugLock = new object();

        public static void Debug(string message, params object[] args)
        {
            if (!isDebugging)
                return;

            lock (debugLock)
            {
                debugLogger.DebugFormat(message, args);
            }
        }

        public static LoggingEvent[] GetLatestDebugMessages()
        {
            lock (debugLock)
            {
                LoggingEvent[] events = memoryAppender.GetEvents();
                memoryAppender.Clear();
                
                return events;
            }
        }

        public static void ClearDebugMessages()
        {
            lock (debugLock)
            {
                memoryAppender.Clear();
            }
        }

        private static bool isDebugging = false;
        public static bool IsDebugging
        {
            get { return isDebugging; }
            set
            {
                isDebugging = value;

                if (isDebugging)
                    ((log4net.Repository.Hierarchy.Logger)debugLogger.Logger).Parent.RemoveAppender(fileAppender);
                else
                    ((log4net.Repository.Hierarchy.Logger)debugLogger.Logger).Parent.AddAppender(fileAppender);
            }
        }
    }
}
