using System;
using Shared.Abstractions.Game.Context.Providers;
using Shared.Abstractions.Game.Runtime.Data;

namespace Shared.Game.Context.Providers
{
    public class RuntimeOrderProvider : IRuntimeOrderProvider
    {
        public IRuntimeOrderData RuntimeData { get; private set; }
        private object orderLock;
        
        public void Sync(IRuntimeOrderData runtimeData)
        {
            orderLock ??= new object();
            lock (orderLock)
            {
                RuntimeData = runtimeData ?? throw new Exception($"{GetType().Name} not initialized.");
            }
        }

        public int Next()
        {
            if (orderLock == null)
                throw new Exception($"{GetType().Name} not initialized.");

            lock (orderLock)
            {
                return RuntimeData.NextOrder ++;
            }
        }

        public void Dispose()
        {
            orderLock = null;
            RuntimeData = null;
        }
    }
}