using System;
using System.Collections.Generic;
using Demo.Core.Abstractions.Game.Runtime.Objects;

namespace Demo.Core.Abstractions.Game.Collections
{
    public interface IRuntimeCollection<TRuntime> : IDisposable, IEnumerable<TRuntime> where TRuntime : IRuntimeObjectBase
    {
        int Count { get; }
        TRuntime this[int index] { get; }
        bool Contains(TRuntime value);
        bool Contains<T>(Predicate<T> predicate) where T : TRuntime;
        bool Contains(int id);
        void Sort(Comparison<TRuntime> comparison);
        void Clear();

        bool Insert(int index, TRuntime value, bool notify = true);
        
        bool Add(TRuntime value, bool notify = true);
        int AddRange(IEnumerable<TRuntime> values, bool notify = true);
        
        bool Remove(int id, bool notify = true);
        bool Remove(TRuntime value, bool notify = true);
        int RemoveRange(IEnumerable<TRuntime> values, bool notify = true);
        int RemoveRange(IEnumerable<int> ids, bool notify = true);

        TRuntime Get(int id);
        T Get<T>(int id) where T : TRuntime;
        bool TryGet(int id, out TRuntime result);
        bool TryGet<T>(int id, out T result) where T : TRuntime;
        bool TryGet(Predicate<TRuntime> predicate, out TRuntime result);
        T GetFirst<T>(Predicate<T> predicate) where T : TRuntime;
        T GetLast<T>(Predicate<T> predicate) where T : TRuntime;

        TRuntime[] GetAll();
        T[] GetAll<T>() where T : TRuntime;
        TRuntime[] GetRange(IEnumerable<int> ids);
        T[] GetRange<T>(IEnumerable<int> ids) where T : TRuntime;
        TRuntime[] GetRange(Predicate<TRuntime> predicate);
        T[] GetRange<T>(Predicate<T> predicate) where T : TRuntime;
    }
}