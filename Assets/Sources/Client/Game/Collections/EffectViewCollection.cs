using Client.Game.Abstractions.Collections;
using Client.Game.Abstractions.Runtime.Views;
using Shared.Game.Collections;

namespace Client.Game.Collections
{
    public class EffectViewCollection : RuntimeCollectionBase<IRuntimeEffectView>, IEffectViewCollection
    {
        protected override int GetId(IRuntimeEffectView value) =>
            value?.Model?.Id ?? int.MaxValue;
    }
}