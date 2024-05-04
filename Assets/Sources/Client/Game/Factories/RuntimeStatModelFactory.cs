using Client.Game.Abstractions.Factories;
using Client.Game.Abstractions.Runtime.Models;
using Client.Game.Runtime.Models;
using Shared.Abstractions.Game.Runtime.Data;
using Shared.Game.Utils;

namespace Client.Game.Factories
{
    public class RuntimeStatModelFactory : IRuntimeStatModelFactory
    {
        public IRuntimeStatModel Create(IRuntimeStatData runtimeData)
        {
            return new RuntimeStatModel().Map(runtimeData);
        }
    }
}