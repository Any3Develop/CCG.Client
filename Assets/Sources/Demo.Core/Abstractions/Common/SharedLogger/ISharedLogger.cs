using System;

namespace Demo.Core.Abstractions.Common.SharedLogger
{
    public interface ISharedLogger
    {
        void Log(string message);
        void Error(string message);
        void Error(Exception exception);
    }
}