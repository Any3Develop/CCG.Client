using Shared.Abstractions.Game.Context.EventSource;

namespace Shared.Game.Context.EventSource
{
    public class EventsSourceFactory : IEventsSourceFactory
    {
        public IEventsSource Create(params object[] args)
        {
            return new EventsSource();
        }
    }
}