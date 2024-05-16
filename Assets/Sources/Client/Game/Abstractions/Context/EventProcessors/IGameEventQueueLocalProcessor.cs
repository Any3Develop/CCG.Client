using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Shared.Abstractions.Game.Events;

namespace Client.Game.Abstractions.Context.EventProcessors
{
    public interface IGameEventQueueLocalProcessor
    {
        UniTask ProcessAsync(IEnumerable<IGameEvent> queue);
        UniTask ProcessAsync(IGameEvent gameEvent);
        UniTask InterruptAsync();
    }
}