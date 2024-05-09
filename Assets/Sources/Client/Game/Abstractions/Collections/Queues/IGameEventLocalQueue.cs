using Shared.Abstractions.Game.Events;

namespace Client.Game.Abstractions.Collections.Queues
{
    public interface IGameEventLocalQueue : IQueue<IGameEvent> {}
}