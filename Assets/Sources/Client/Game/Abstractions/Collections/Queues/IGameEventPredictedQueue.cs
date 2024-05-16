using Shared.Abstractions.Game.Events;

namespace Client.Game.Abstractions.Collections.Queues
{
    public interface IGameEventPredictedQueue : IQueue<IGameEvent> {}
}