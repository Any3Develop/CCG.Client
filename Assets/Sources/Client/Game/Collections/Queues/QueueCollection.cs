using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Client.Game.Abstractions.Collections.Queues;

namespace Client.Game.Collections.Queues
{
    public class QueueCollection<T> : IQueue<T>
    {
        protected Queue<T> Collection = new();
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

        public bool TryPeek(out T result)
        {
            if (Count != 0)
            {
                result = Peek();
                return true;
            }

            result = default;
            return false;
        }

        public T Peek()
        {
            return Count == 0 ? default : Collection.Peek();
        }

        public void Reverse()
        {
            Collection = new Queue<T>(Collection.Reverse());
        }

        public virtual void Clear()
        {
            Collection.Clear();
        }

        public IEnumerator<T> GetEnumerator() => Collection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}