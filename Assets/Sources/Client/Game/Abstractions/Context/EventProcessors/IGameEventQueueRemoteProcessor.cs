using System.Collections.Generic;
using Shared.Abstractions.Game.Events;

namespace Client.Game.Abstractions.Context.EventProcessors
{
    public interface IGameEventQueueRemoteProcessor
    {
        void Process(IEnumerable<IGameEvent> queue);
    }
}