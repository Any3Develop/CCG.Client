using System.Collections.Generic;
using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Abstractions.Game.Collections
{
    public interface IRuntimeEffectCollection : IDictionary<int, IRuntimeEffect>
    {
        int AddRange(IEnumerable<IRuntimeEffect> values);
        int RemoveRange(IEnumerable<int> ids);
        int RemoveRange(IEnumerable<IRuntimeEffect> values);
    }
}