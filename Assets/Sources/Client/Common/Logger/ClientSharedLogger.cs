using Shared.Abstractions.Common.Logger;
using Shared.Common.Logger;
using UnityEngine;

namespace Client.Common.Logger
{
    public class ClientSharedLogger : ISharedLogger
    {
        public ClientSharedLogger()
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