using UnityEngine;

namespace Core.Base.Logger
{
    public class UnityLoggerWithPrefix : UnityLoggerDecorator
    {
        private readonly object m_Prefix;

        internal UnityLoggerWithPrefix(ILogger logger, object prefix) : base(logger)
        {
            m_Prefix = prefix;
        }

        public override void Log(object message)
        {
            base.Log(message.AddPrefix(m_Prefix));
        }

        public override void Log(LogType logType, object message)
        {
            base.Log(logType, message.AddPrefix(m_Prefix));
        }
    }
}