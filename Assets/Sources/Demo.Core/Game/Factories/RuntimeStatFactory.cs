using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Abstractions.Game.Factories;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Game.Factories
{
    public class RuntimeStatFactory : IRuntimeFactory<IRuntimeStat>
    {
        public IRuntimeStat Create(string ownerId, IData data)
        {
            throw new System.NotImplementedException();
        }

        public IRuntimeStat Create(IRuntimeData runtimeData)
        {
            throw new System.NotImplementedException();
        }
    }
}