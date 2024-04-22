using System.Collections.Generic;
using Demo.Core.Abstractions.Game.Data;

namespace Demo.Core.Abstractions.Game.Collections
{
    public interface IDatabaseCollection
    {
        void Init(IEnumerable<IDatabase> values);
        IDatabase GetFirst(string id);
        T Get<T>(string id);
        T[] GetAll<T>();
        T[] GetRange<T>(IEnumerable<string> ids);
    }
}