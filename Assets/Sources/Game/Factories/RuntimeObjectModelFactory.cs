using System;
using System.Linq;
using Client.Game.Abstractions.Factories;
using Client.Game.Abstractions.Runtime.Models;
using Client.Game.Runtime.Models;
using Client.Services.DIService;
using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context;
using Shared.Abstractions.Game.Runtime.Data;
using Shared.Game.Data;
using Shared.Game.Data.Enums;
using Shared.Game.Utils;

namespace Client.Game.Factories
{
    public class RuntimeObjectModelFactory : IRuntimeObjectModelFactory
    {
        private readonly IAbstractFactory abstractFactory;
        private readonly IRuntimeStatModelFactory runtimeStatModelFactory;
        private readonly IDatabase database;

        public RuntimeObjectModelFactory(
            IAbstractFactory abstractFactory,
            IRuntimeStatModelFactory runtimeStatModelFactory, 
            IDatabase database)
        {
            this.abstractFactory = abstractFactory;
            this.runtimeStatModelFactory = runtimeStatModelFactory;
            this.database = database;
        }

        public IRuntimeObjectModel Create(IRuntimeObjectData runtimeData)
        {
            if (!database.Objects.TryGet(runtimeData.DataId, out var data))
                throw new NullReferenceException($"{nameof(ObjectData)} with id {runtimeData.DataId}, not found in {nameof(IDataCollection<ObjectData>)}");

            return data.Type switch
            {
                ObjectType.Creature or ObjectType.Spell => CreateCardModel(runtimeData),
                _ => throw new NotImplementedException($"Unknown {nameof(ObjectType)}: {data.Type}")
            };
        }

        private IRuntimeObjectModel CreateCardModel(IRuntimeObjectData runtimeData)
        {
            if (!database.Objects.TryGet<CardData>(runtimeData.DataId, out var data))
                throw new NullReferenceException($"{nameof(CardData)} with id {runtimeData.DataId}, not found in {nameof(IDataCollection<ObjectData>)}");

            var model = abstractFactory.Instantiate<RuntimeCardModel>();
            model.Data = data;
            model.Stats = runtimeData.Stats.Select(runtimeStatModelFactory.Create).ToList();
            return model.Map(runtimeData);
        }
    }
}