using System.Collections.Generic;
using Shared.Abstractions.Game.Events;

namespace Client.Game.Abstractions.Context.Logic.Queue
{
    public interface IGameEventQueueProcessor
    {
        void Process(IEnumerable<IGameEvent> queue);
        void Stop();
    }
}