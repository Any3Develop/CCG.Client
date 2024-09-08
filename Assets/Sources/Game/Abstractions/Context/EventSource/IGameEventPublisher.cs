using Cysharp.Threading.Tasks;
using Shared.Abstractions.Game.Events;

namespace Client.Game.Abstractions.Context.EventSource
{
    public interface IGameEventPublisher
    {
        void Publish(IGameEvent value);
        UniTask PublishAsync(IGameEvent value);
        UniTask PublishParallelAsync(IGameEvent value);
    }
}