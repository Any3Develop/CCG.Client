using Demo.Core.Abstractions.Common.Logger;
using Demo.Core.Common.Logger;
using UnityEngine;

namespace Common.Logger
{
    public class UnityLogger : ISharedLogger
    {
        public UnityLogger()
        {
            SharedLogger.Initialize(this);
        }

        public void Log(object message)
        {
            Debug.Log(message);
        }

        public void Warning(object message)
        {
            Debug.LogWarning(message);
        }

        public void Error(object message)
        {
            Debug.LogError(message);
        }
    }
}