using System.Collections.Generic;

namespace Client.Game.Abstractions.Collections.Queues
{
    public interface IQueue<T> : IEnumerable<T>
    {
        int Count { get; }
        T Dequeue();
        void Enqueue(T value);
        void Enqueue(IEnumerable<T> values);
        void Reverse();
        void Clear();
    }
}