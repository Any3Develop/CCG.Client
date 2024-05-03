using Shared.Abstractions.Common.Logger;

namespace Shared.Common.Logger
{
    public class SharedLogger
    {
        private static ISharedLogger logger;

        public static void Initialize(ISharedLogger instance)
        {
            logger = instance;
        }

        public static void Log(object message)
        {
            logger.Log(message);
        }

        public static void Warning(object message)
        {
            logger.Warning(message);
        }

        public static void Error(object message)
        {
            logger.Error(message);
        }
    }
}