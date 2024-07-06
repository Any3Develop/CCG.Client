namespace Client.Common.Services.EventSourceService.Realizations
{
    public class EventSourceFactory : IEventSourceFactory
    {
        public IEventSource Crete()
        {
            return new UnitaskBasedEventSource();
        }
    }
}