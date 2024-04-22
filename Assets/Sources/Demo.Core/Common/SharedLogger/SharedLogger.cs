using System;
using Demo.Core.Abstractions.Common.SharedLogger;

namespace Demo.Core.Common.SharedLogger
{
    public class SharedLogger
    {
        private static ISharedLogger logger;

        public static void Initialize(ISharedLogger instance)
        {
            logger = instance;
        }

        public static void Log(string message)
        {
            logger.Log(message);
        }

        public static void Error(string message)
        {
            logger.Error(message);
        }

        public static void Error(Exception exception)
        {
            logger.Error(exception);
        }
    }
}