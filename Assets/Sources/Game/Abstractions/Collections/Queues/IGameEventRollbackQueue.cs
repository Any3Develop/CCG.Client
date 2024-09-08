using Shared.Abstractions.Game.Events;

namespace Client.Game.Abstractions.Collections.Queues
{
    public interface IGameEventRollbackQueue : IQueue<IGameEvent> {}
}