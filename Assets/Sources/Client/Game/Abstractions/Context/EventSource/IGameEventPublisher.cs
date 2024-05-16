using Cysharp.Threading.Tasks;
using Shared.Abstractions.Game.Events;

namespace Client.Game.Abstractions.Context.EventSource
{
    public interface IGameEventPublisher
    {
        UniTask PublishAsync<T>(T value) where T : IGameEvent;
    }
}