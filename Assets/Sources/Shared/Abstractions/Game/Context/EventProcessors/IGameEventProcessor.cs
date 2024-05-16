using Shared.Abstractions.Game.Events;

namespace Shared.Abstractions.Game.Context.EventProcessors
{
    public interface IGameEventProcessor
    {
        void Process(IGameEvent gameEvent);
    }
}