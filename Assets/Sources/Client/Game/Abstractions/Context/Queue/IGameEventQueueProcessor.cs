using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Shared.Abstractions.Game.Events;

namespace Client.Game.Abstractions.Context.Queue
{
    public interface IGameEventQueueProcessor
    {
        void Register(IEnumerable<IGameEvent> queue);
        void StartProcess();
        UniTask ProcessAsync(IGameEvent gameEvent);
        UniTask InterruptAsync();
    }
}