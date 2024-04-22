using System.Collections.Generic;
using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Abstractions.Game.Collections
{
    public interface IRuntimeObjectCollection : IDictionary<int, IRuntimeObject>
    {
        int AddRange(IEnumerable<IRuntimeObject> values);
        int RemoveRange(IEnumerable<int> ids);
        int RemoveRange(IEnumerable<IRuntimeObject> values);
    }
}