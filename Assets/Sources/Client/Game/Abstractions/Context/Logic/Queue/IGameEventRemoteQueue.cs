using Client.Game.Abstractions.Collections;
using Shared.Abstractions.Game.Events;

namespace Client.Game.Abstractions.Context.Logic.Queue
{
    public interface IGameEventRemoteQueue : IQueue<IGameEvent> {}
}