using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Client.Game.Abstractions.Collections.Queues;

namespace Client.Game.Collections.Queues
{
    public class ReverseQueueCollection<T> : IQueue<T>
    {
        protected Stack<T> Collection = new();
        public virtual int Count => Collection.Count;

        public virtual T Dequeue()
        {
            return Collection.Pop();
        }

        public virtual void Enqueue(T value)
        {
            Collection.Push(value);
        }

        public virtual void Enqueue(IEnumerable<T> values)
        {
            foreach (var value in values)
                Enqueue(value);
        }

        public void Reverse()
        {
            Collection = new Stack<T>(Collection.Reverse());
        }

        public virtual void Clear()
        {
            Collection.Clear();
        }

        public IEnumerator<T> GetEnumerator() => Collection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}