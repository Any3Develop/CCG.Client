using Client.Game.Abstractions.Factories;
using Client.Game.Abstractions.Runtime.Models;
using Client.Game.Runtime.Models;
using Client.Services.DIService;
using Shared.Abstractions.Game.Runtime.Data;
using Shared.Game.Utils;

namespace Client.Game.Factories
{
    public class RuntimeStatModelFactory : IRuntimeStatModelFactory
    {
        private readonly IAbstractFactory abstractFactory;
        public RuntimeStatModelFactory(IAbstractFactory abstractFactory)
        {
            this.abstractFactory = abstractFactory;
        }

        public IRuntimeStatModel Create(IRuntimeStatData runtimeData)
        {
            return abstractFactory.Instantiate<RuntimeStatModel>().Map(runtimeData);
        }
    }
}