using System;
using Client.Common.Abstractions.DependencyInjection;
using Client.Game.Abstractions.Factories;
using Client.Game.Abstractions.Runtime.Models;
using Client.Game.Runtime.Models.Effects;
using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context;
using Shared.Abstractions.Game.Runtime.Data;
using Shared.Game.Data;
using Shared.Game.Utils;

namespace Client.Game.Factories
{
    public class RuntimeEffectModelFactory : IRuntimeEffectModelFactory
    {
        private readonly IAbstractFactory abstractFactory;
        private readonly IDatabase database;

        public RuntimeEffectModelFactory(
            IAbstractFactory abstractFactory,
            IDatabase database)
        {
            this.abstractFactory = abstractFactory;
            this.database = database;
        }

        public IRuntimeEffectModel Create(IRuntimeEffectData runtimeData)
        {
            if (!database.Effects.TryGet(runtimeData.DataId, out var data))
                throw new NullReferenceException($"{nameof(EffectData)} with id {runtimeData.DataId}, not found in {nameof(IDataCollection<EffectData>)}");
            
            var model = abstractFactory.Instantiate<RuntimeEffectModel>();
            model.Data = data;
            return model.Map(runtimeData);
        }
    }
}