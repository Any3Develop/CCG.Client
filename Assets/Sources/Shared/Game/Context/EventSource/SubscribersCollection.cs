using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Game.Context.EventSource
{
    public class SubscribersCollection : List<Subscriber>, IDisposable
    {
        public bool UnSorted { get; set; }

        public new void Clear()
        {
            UnSorted = false;
            var disposables = this.OfType<IDisposable>().ToArray();
            base.Clear();
            
            foreach (var disposable in disposables)
                disposable?.Dispose();
        }
        
        public void Dispose()
        {
            Clear();
        }
    }
}