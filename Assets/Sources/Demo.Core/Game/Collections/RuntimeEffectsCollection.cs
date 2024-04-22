using System.Collections.Generic;
using System.Linq;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Game.Collections
{
    public class RuntimeEffectsCollection : Dictionary<int, IRuntimeEffect>, IRuntimeEffectCollection
    {
        public RuntimeEffectsCollection(IEnumerable<IRuntimeEffect> preInit = null)
        {
            AddRange(preInit);
        }

        public int AddRange(IEnumerable<IRuntimeEffect> values)
        {
            return values?
                .Where(value => value?.RuntimeData != null)
                .Count(value => TryAdd(value.RuntimeData.Id, value)) ?? 0;
        }

        public int RemoveRange(IEnumerable<int> ids)
        {
            return ids?.Count(Remove) ?? 0;
        }

        public int RemoveRange(IEnumerable<IRuntimeEffect> values)
        {
            return values?
                .Where(value => value?.RuntimeData != null)
                .Count(value => Remove(value.RuntimeData.Id)) ?? 0;
        }
    }
}