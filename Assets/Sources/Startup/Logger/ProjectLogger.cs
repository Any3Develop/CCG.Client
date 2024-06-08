using Shared.Abstractions.Common.Logger;
using Shared.Common.Logger;
using UnityEngine;

namespace Startup.Logger
{
    public class ProjectLogger : ISharedLogger
    {
        public ProjectLogger()
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