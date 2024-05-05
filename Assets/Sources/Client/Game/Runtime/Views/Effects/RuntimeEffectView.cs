using Client.Game.Abstractions.Runtime.Models;
using Client.Game.Abstractions.Runtime.Views;
using Cysharp.Threading.Tasks;

namespace Client.Game.Runtime.Views.Effects
{
    public abstract class RuntimeEffectView : RuntimeView, IRuntimeEffectView
    {
        public new IRuntimeEffectModel Model => (IRuntimeEffectModel) base.Model;

        public UniTask ChangeAsync(IRuntimeEffectModel model)
        {
            Setup(model);
            return OnChangedAsync();
        }

        public virtual UniTask AppliedAsync() => UniTask.CompletedTask;

        public virtual UniTask ExpireAsync() => UniTask.CompletedTask;

        public virtual UniTask StartAsync() => UniTask.CompletedTask;

        public virtual UniTask EndAsync() => UniTask.CompletedTask;
        protected virtual UniTask OnChangedAsync() => UniTask.CompletedTask;
    }
}