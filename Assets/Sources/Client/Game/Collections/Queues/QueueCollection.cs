using System.Collections;
using System.Collections.Generic;
using Client.Game.Abstractions.Collections.Queues;

namespace Client.Game.Collections.Queues
{
    public class QueueCollection<T> : IQueue<T>
    {
        protected readonly Queue<T> Collection = new();
        public virtual int Count => Collection.Count;

        public virtual T Dequeue()
        {
            return Collection.Dequeue();
        }

        public virtual void Enqueue(T value)
        {
            Collection.Enqueue(value);
        }

        public virtual void Enqueue(IEnumerable<T> values)
        {
            foreach (var value in values)
                Enqueue(value);
        }

        public virtual void Clear()
        {
            Collection.Clear();
        }

        public IEnumerator<T> GetEnumerator() => Collection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}