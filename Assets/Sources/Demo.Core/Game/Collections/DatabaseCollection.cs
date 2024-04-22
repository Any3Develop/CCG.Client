using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.Data;

namespace Demo.Core.Game.Collections
{
    public class DatabaseCollection : List<IDatabase>, IDatabaseCollection
    {
        public void Init(IEnumerable<IDatabase> values)
        {
            Clear();
            AddRange(values);
        }

        public IDatabase GetFirst(string id)
        {
            return this.FirstOrDefault(o => o.Id == id);
        }

        public T Get<T>(string id)
        {
            return (T)this.FirstOrDefault(o => o.Id == id);
        }

        public T[] GetAll<T>()
        {
            return this.OfType<T>().ToArray();
        }

        public T[] GetRange<T>(IEnumerable<string> ids)
        {
            return ids != null
                ? this.Where(o => ids.Contains(o.Id)).OfType<T>().ToArray()
                : Array.Empty<T>();
        }
    }
}