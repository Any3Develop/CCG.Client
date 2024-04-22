using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Abstractions.Game.Factories;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Game.Factories
{
    public class RuntimeEffectFactory : IRuntimeFactory<IRuntimeEffect>
    {
        public IRuntimeEffect Create(string ownerId, IDatabase data)
        {
            throw new System.NotImplementedException();
        }

        public IRuntimeEffect Create(IRuntimeData runtimeData)
        {
            throw new System.NotImplementedException();
        }
    }
}