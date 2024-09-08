using Client.Game.Abstractions.Runtime.Models;
using Cysharp.Threading.Tasks;

namespace Client.Game.Abstractions.Runtime.Views
{
    public interface IRuntimeEffectView : IRuntimeView
    {
        new IRuntimeEffectModel Model { get; }
        UniTask ChangeAsync(IRuntimeEffectModel model);
        UniTask AppliedAsync();
        UniTask ExpireAsync();
        UniTask StartAsync();
        UniTask EndAsync();
    }
}