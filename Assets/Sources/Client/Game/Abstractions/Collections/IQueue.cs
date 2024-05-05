using System.Collections.Generic;

namespace Client.Game.Abstractions.Collections
{
    public interface IQueue<T> : IEnumerable<T>
    {
        int Count { get; }
        T this[int index] { get; }
        T Dequeue();
        void Enqueue(T value);
        void Enqueue(IEnumerable<T> values);
        void Clear();
    }
}