using System.Collections.Generic;
using System.Linq;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Game.Collections
{
    public class RuntimeStatsCollection : Dictionary<string, IRuntimeStat>, IRuntimeStatsCollection
    {
        public RuntimeStatsCollection(IEnumerable<IRuntimeStat> preInit = null)
        {
            AddRange(preInit);
        }

        public int AddRange(IEnumerable<IRuntimeStat> values)
        {
            return values?
                .Where(value => !string.IsNullOrWhiteSpace(value?.RuntimeData?.Name))
                .Count(value => TryAdd(value.RuntimeData.Name, value)) ?? 0;
        }

        public int RemoveRange(IEnumerable<string> ids)
        {
            return ids?
                .Where(key => !string.IsNullOrWhiteSpace(key))
                .Count(Remove) ?? 0;
        }

        public int RemoveRange(IEnumerable<IRuntimeStat> values)
        {
            return values?
                .Where(value => !string.IsNullOrWhiteSpace(value?.RuntimeData?.Name))
                .Count(value => Remove(value.RuntimeData.Name)) ?? 0;
        }
    }
}