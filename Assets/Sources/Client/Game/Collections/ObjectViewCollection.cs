using Client.Game.Abstractions.Collections;
using Client.Game.Abstractions.Runtime.Views;
using Shared.Game.Collections;

namespace Client.Game.Collections
{
    public class ObjectViewCollection : RuntimeCollectionBase<IRuntimeObjectView>, IObjectViewCollection
    {
        protected override int GetId(IRuntimeObjectView value) =>
            value?.Model?.Id ?? int.MaxValue;
    }
}