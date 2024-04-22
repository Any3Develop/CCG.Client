using System;
using System.Collections.Generic;
using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Abstractions.Game.Collections
{
    public interface IRuntimeCollection<TRuntime> : IDisposable, IEnumerable<TRuntime> where TRuntime : IRuntimeBase
    {
        int Count { get; }
        TRuntime this[int index] { get; }
        bool Contains(TRuntime value);
        bool Contains(Predicate<TRuntime> predicate);
        bool Contains(int id);
        void Sort(Comparison<TRuntime> comparison);
        void Clear();

        bool Insert(int index, TRuntime value);
        
        bool Add(TRuntime value);
        int AddRange(IEnumerable<TRuntime> values);
        
        bool Remove(int id);
        bool Remove(TRuntime value);
        int RemoveRange(IEnumerable<TRuntime> values);
        int RemoveRange(IEnumerable<int> ids);
        
        TRuntime Get(int id);
        TRuntime GetFirst(Predicate<TRuntime> predicate);
        TRuntime GetLast(Predicate<TRuntime> predicate);

        TRuntime[] GetAll();
        TRuntime[] GetRange(IEnumerable<int> ids);
        TRuntime[] GetRange(Predicate<TRuntime> predicate);
    }
}