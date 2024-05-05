using System.Collections.Generic;
using System.Threading;
using Shared.Abstractions.Game.Events;

namespace Client.Game.Abstractions.Context.Logic.Queue
{
    public interface IGameEventQueueLocalProcessor
    {
        void ProcessAsync(IEnumerable<IGameEvent> queue, CancellationToken token);
    }
}