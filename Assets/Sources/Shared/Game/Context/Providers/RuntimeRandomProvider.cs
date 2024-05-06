using System;
using Shared.Abstractions.Game.Context.Providers;

namespace Shared.Game.Context.Providers
{
    public class RuntimeRandomProvider : RuntimeNumberProvider, IRuntimeRandomProvider
    {
        private Random random;
        public RuntimeRandomProvider(int seed = 99)
        {
            random = new Random(seed);
        }
        
        public override int Next() => random.Next();

        public override void Sync(int seed)
        {
            base.Sync(seed);
            random = new Random(seed);
        }
    }
}