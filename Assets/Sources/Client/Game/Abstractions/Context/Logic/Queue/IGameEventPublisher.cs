using Cysharp.Threading.Tasks;
using Shared.Abstractions.Game.Events;

namespace Client.Game.Abstractions.Context.Logic.Queue
{
    public interface IGameEventPublisher
    {
        UniTask PublishAsync(IGameEvent value);
    }
}