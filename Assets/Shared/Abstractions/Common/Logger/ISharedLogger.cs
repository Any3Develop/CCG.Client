namespace Shared.Abstractions.Common.Logger
{
    public interface ISharedLogger
    {
        void Log(object message);
        void Warning(object message);
        void Error(object message);
    }
}