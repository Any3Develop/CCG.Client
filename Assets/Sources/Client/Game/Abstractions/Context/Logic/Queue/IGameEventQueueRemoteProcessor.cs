using System.Collections.Generic;
using Shared.Abstractions.Game.Events;

namespace Client.Game.Abstractions.Context.Logic.Queue
{
    public interface IGameEventQueueRemoteProcessor
    {
        void Process(IEnumerable<IGameEvent> queue);
    }
}