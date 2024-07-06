namespace Client.Common.Services.EventSourceService
{
    public interface IEventSourceFactory
    {
        IEventSource Crete();
    }
}