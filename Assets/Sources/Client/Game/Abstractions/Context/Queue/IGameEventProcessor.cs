using Shared.Abstractions.Game.Events;

namespace Client.Game.Abstractions.Context.Queue
{
    public interface IGameEventProcessor
    {
        void Process(IGameEvent gameEvent);
    }
}