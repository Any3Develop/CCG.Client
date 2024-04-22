using System;
using Demo.Core.Abstractions.Common.SharedLogger;
using Demo.Core.Common.SharedLogger;
using UnityEngine;

namespace Common.Logger
{
    public class UnityLogger : ISharedLogger
    {
        public UnityLogger()
        {
            DefaultSharedLogger.Initialize(this);
        }

        public void Log(string message)
        {
            Debug.Log(message);
        }

        public void Error(string message)
        {
            Debug.LogError(message);
        }

        public void Error(Exception exception)
        {
            Debug.LogException(exception);
        }
    }
}