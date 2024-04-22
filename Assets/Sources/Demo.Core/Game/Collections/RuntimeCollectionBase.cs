using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Game.Collections
{
    public abstract class RuntimeCollectionBase<T> : IRuntimeCollection<T> where T : IRuntimeBase
    {
        protected readonly List<T> Collection = new();
        public virtual int Count => Collection.Count;

        public virtual T this[int index] => Collection[index];

        public virtual void Dispose()
        {
            Collection?.ForEach(x => x?.Dispose());
            Clear();
        }
        
        public virtual bool Contains(T value)
        {
            return value?.RuntimeData != null
                   && Contains(value.RuntimeData.Id);
        }

        public virtual bool Contains(Predicate<T> predicate)
        {
            return Collection.Any(predicate.Invoke);
        }

        public virtual bool Contains(int id)
        {
            return Collection.Any(x => x.RuntimeData.Id == id);
        }

        public virtual void Sort(Comparison<T> comparison) => Collection.Sort(comparison);

        public virtual void Clear() => Collection.Clear();

        public virtual bool Insert(int index, T value)
        {
            if (index < 0 || index > Count || value?.RuntimeData == null || Contains(value))
                return false;

            Collection.Insert(index, value);
            return true;
        }

        public virtual bool Add(T value)
        {
            if (value == null || Contains(value))
                return false;

            Collection.Add(value);
            return true;
        }

        public virtual int AddRange(IEnumerable<T> values)
        {
            return values?.Count(Add) ?? 0;
        }

        public virtual bool Remove(int id)
        {
            return Collection.RemoveAll(x => x.RuntimeData.Id == id) > 0;
        }

        public virtual bool Remove(T value)
        {
            return value?.RuntimeData != null && Remove(value.RuntimeData.Id);
        }

        public virtual int RemoveRange(IEnumerable<T> values)
        {
            return values?.Count(Remove) ?? 0;
        }

        public virtual int RemoveRange(IEnumerable<int> ids)
        {
            return ids?.Count(Remove) ?? 0;
        }

        public virtual T Get(int id)
        {
            return Collection.Find(x => x.RuntimeData.Id == id);
        }

        public bool TryGet(int id, out T result)
        {
            result = Get(id);
            return result != null;
        }

        public virtual T GetFirst(Predicate<T> predicate)
        {
            return Collection.Find(predicate);
        }

        public virtual T GetLast(Predicate<T> predicate)
        {
            return Collection.FindLast(predicate);
        }

        public virtual T[] GetAll()
        {
            return Collection.ToArray();
        }

        public virtual T[] GetRange(IEnumerable<int> ids)
        {
            return ids == null ? Array.Empty<T>() : Collection.Where(x => ids.Contains(x.RuntimeData.Id)).ToArray();
        }

        public virtual T[] GetRange(Predicate<T> predicate)
        {
            return Collection.Where(predicate.Invoke).ToArray();
        }

        public IEnumerator<T> GetEnumerator() => Collection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}