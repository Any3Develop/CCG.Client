using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Runtime.Objects;

namespace Shared.Game.Collections
{
    public class RuntimeCollection<TRuntime> : IRuntimeCollection<TRuntime> where TRuntime : IRuntimeObjectBase
    {
        protected readonly List<TRuntime> Collection = new();
        public virtual int Count => Collection.Count;

        public virtual TRuntime this[int index] => Collection[index];

        public virtual void Dispose()
        {
            Collection?.ForEach(x => x?.Dispose());
            Clear();
        }
        
        public virtual bool Contains(TRuntime value)
        {
            return value?.RuntimeData != null
                   && Contains(value.RuntimeData.Id);
        }

        public bool Contains<T>(Predicate<T> predicate) where T : TRuntime
        {
            return Collection.OfType<T>().Any(predicate.Invoke);
        }
        
        public virtual bool Contains(int id)
        {
            return Collection.Any(x => x.RuntimeData.Id == id);
        }

        public virtual void Sort(Comparison<TRuntime> comparison) => Collection.Sort(comparison);

        public virtual void Clear() => Collection.Clear();

        public virtual bool Insert(int index, TRuntime value, bool notify = true)
        {
            if (index < 0 || index > Count || value?.RuntimeData == null || Contains(value))
                return false;

            Collection.Insert(index, value);
            return true;
        }

        public virtual bool Add(TRuntime value, bool notify = true)
        {
            if (value == null || Contains(value))
                return false;

            Collection.Add(value);
            return true;
        }

        public virtual int AddRange(IEnumerable<TRuntime> values, bool notify = true)
        {
            return values?.Count(value => Add(value, notify)) ?? 0;
        }

        public virtual bool Remove(int id, bool notify = true)
        {
            return Collection.RemoveAll(x => x.RuntimeData.Id == id) > 0;
        }

        public virtual bool Remove(TRuntime value, bool notify = true)
        {
            return value?.RuntimeData != null && Remove(value.RuntimeData.Id, notify);
        }

        public virtual int RemoveRange(IEnumerable<TRuntime> values, bool notify = true)
        {
            return values?.Count(value => Remove(value, notify)) ?? 0;
        }

        public virtual int RemoveRange(IEnumerable<int> ids, bool notify = true)
        {
            return ids?.Count(value => Remove(value, notify)) ?? 0;
        }

        public virtual TRuntime Get(int id)
        {
            return Collection.FirstOrDefault(x => x.RuntimeData.Id == id);
        }

        public T Get<T>(int id) where T : TRuntime
        {
            return Collection.OfType<T>().FirstOrDefault(x => x.RuntimeData.Id == id);
        }

        public bool TryGet(int id, out TRuntime result)
        {
            result = Get(id);
            return result != null;
        }

        public bool TryGet<T>(int id, out T result) where T : TRuntime
        {
            result = Get<T>(id);
            return result != null;
        }

        public bool TryGet(Predicate<TRuntime> predicate, out TRuntime result)
        {
            result = GetFirst(predicate);
            return result != null;
        }

        public T GetFirst<T>(Predicate<T> predicate) where T : TRuntime
        {
            return Collection.OfType<T>().FirstOrDefault(predicate.Invoke);
        }

        public T GetLast<T>(Predicate<T> predicate) where T : TRuntime
        {
            return Collection.OfType<T>().LastOrDefault(predicate.Invoke);
        }

        public virtual TRuntime GetFirst(Predicate<TRuntime> predicate)
        {
            return Collection.Find(predicate);
        }

        public virtual TRuntime GetLast(Predicate<TRuntime> predicate)
        {
            return Collection.FindLast(predicate);
        }

        public virtual TRuntime[] GetAll()
        {
            return Collection.ToArray();
        }

        public T[] GetAll<T>() where T : TRuntime
        {
            return Collection.OfType<T>().ToArray();
        }

        public virtual TRuntime[] GetRange(IEnumerable<int> ids)
        {
            return ids == null 
                ? Array.Empty<TRuntime>() 
                : Collection.Where(x => ids.Contains(x.RuntimeData.Id)).ToArray();
        }

        public T[] GetRange<T>(IEnumerable<int> ids) where T : TRuntime
        {
            return ids == null 
                ? Array.Empty<T>() 
                : Collection.OfType<T>().Where(x => ids.Contains(x.RuntimeData.Id)).ToArray();
        }

        public virtual TRuntime[] GetRange(Predicate<TRuntime> predicate)
        {
            return Collection.Where(predicate.Invoke).ToArray();
        }

        public T[] GetRange<T>(Predicate<T> predicate) where T : TRuntime
        {
            return Collection.OfType<T>().Where(predicate.Invoke).ToArray();
        }

        #region IEnumerable
        public IEnumerator<TRuntime> GetEnumerator() => Collection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion
    }
}