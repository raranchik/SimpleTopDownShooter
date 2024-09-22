using UnityEngine;

namespace Core.Base.Logger
{
    public static class LoggerExtensions
    {
        public static ILogger WithPrefix(this ILogger logger, object prefix)
        {
            prefix = $"[{prefix}] ";
            return new UnityLoggerWithPrefix(logger, prefix);
        }

        public static object AddPrefix(this object message, object prefix)
        {
            return $"{prefix} {message}";
        }
    }
}