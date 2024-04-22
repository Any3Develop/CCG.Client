using System.Collections.Generic;
using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Abstractions.Game.Collections
{
    public interface IRuntimeStatsCollection : IDictionary<string, IRuntimeStat>
    {
        int AddRange(IEnumerable<IRuntimeStat> values);
        int RemoveRange(IEnumerable<string> ids);
        int RemoveRange(IEnumerable<IRuntimeStat> values);
    }
}