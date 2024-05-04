using System;

namespace Shared.Game.Context.Logic.EventSource
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

        public int CompareTo(Subscriber other) => Order.CompareTo(other.Order);
    }
}