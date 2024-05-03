using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Common.EventSource
{
    public class SubscriberCollection : List<Subscriber>, IDisposable
    {
        public bool UnSorted { get; set; }
        public void Dispose()
        {
            UnSorted = false;
            var disposables = this.OfType<IDisposable>().ToArray();
            Clear();
            
            foreach (var disposable in disposables)
                disposable?.Dispose();
        }
    }
}