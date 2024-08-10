namespace Shared.Abstractions.Game.Context.EventSource
{
    public interface IEventsSourceFactory
    {
        IEventsSource Create(params object[] args);
    }
}