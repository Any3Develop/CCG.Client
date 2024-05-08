using System;
using Shared.Abstractions.Game.Context.Providers;
using Shared.Abstractions.Game.Runtime.Data;

namespace Shared.Game.Context.Providers
{
    public class RuntimeRandomProvider : IRuntimeRandomProvider
    {
        public IRuntimeRandomData RuntimeData { get; private set; }
        private object randomLock;
        private Random random;

        public void Sync(IRuntimeRandomData runtimeData)
        {
            randomLock ??= new object();
            lock (randomLock)
            {
                RuntimeData = runtimeData ?? throw new Exception($"{GetType().Name} not initialized.");
                random = new Random(runtimeData.Seed);
            }
        }

        public int Next()
        {
            if (randomLock == null)
                throw new Exception($"{GetType().Name} not initialized.");

            lock (randomLock)
            {
                return random.Next();
            }
        }

        public void Dispose()
        {
            randomLock = null;
            RuntimeData = null;
            random = null;
        }
    }
}