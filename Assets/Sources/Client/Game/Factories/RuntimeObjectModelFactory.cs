using System;
using System.Linq;
using Client.Game.Abstractions.Factories;
using Client.Game.Abstractions.Runtime.Models;
using Client.Game.Runtime.Models;
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
        private readonly IRuntimeStatModelFactory runtimeStatModelFactory;
        private readonly IDatabase database;

        public RuntimeObjectModelFactory(
            IRuntimeStatModelFactory runtimeStatModelFactory, 
            IDatabase database)
        {
            this.runtimeStatModelFactory = runtimeStatModelFactory;
            this.database = database;
        }

        public IRuntimeObjectModel Create(IRuntimeObjectData runtimeData)
        {
            if (!database.Objects.TryGet(runtimeData.DataId, out var data))
                throw new NullReferenceException($"{nameof(ObjectData)} with id {runtimeData.DataId}, not found in {nameof(IDataCollection<ObjectData>)}");

            return data.Type switch
            {
                ObjectType.Creature or ObjectType.Spell => new RuntimeCardModel
                {
                    Data = (CardData)data,
                    Stats = runtimeData.Stats.Select(runtimeStatModelFactory.Create).ToList()
                }.Map(runtimeData),
                _ => throw new NotImplementedException($"Unknown {nameof(ObjectType)}: {data.Type}")
            };
        }
    }
}