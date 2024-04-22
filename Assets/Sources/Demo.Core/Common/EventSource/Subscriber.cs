using System;

namespace Demo.Core.Common.EventSource
{
    public class Subscriber : IDisposable, IComparable<Subscriber>
    {
        public Delegate Callback;
        public event Action OnDisposeAction;
        public bool HasParameters { get; set; }
        public int Order { get; set; }

        public void Dispose()
        {
            if (OnDisposeAction == null) // prevent dead lock
                return;

            var memo = OnDisposeAction;
            Callback = null;
            OnDisposeAction = null;
            memo?.Invoke();
        }

        public int CompareTo(Subscriber other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Order.CompareTo(other.Order);
        }
    }
}