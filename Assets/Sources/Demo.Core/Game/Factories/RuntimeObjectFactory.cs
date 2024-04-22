using System;
using System.Linq;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.Context;
using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Abstractions.Game.Factories;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Abstractions.Game.RuntimeObjects;
using Demo.Core.Game.Data;
using Demo.Core.Game.Enums;
using Demo.Core.Game.RuntimeData;
using Demo.Core.Game.RuntimeObjects;

namespace Demo.Core.Game.Factories
{
    public class RuntimeObjectFactory : IRuntimeFactory<IRuntimeObject>
    {
        private readonly IDatabase database;
        private readonly IRuntimeIdGenerator runtimeIdGenerator;
        private readonly IContextFactory contextFactory;

        public RuntimeObjectFactory(
            IDatabase database,
            IRuntimeIdGenerator runtimeIdGenerator,
            IContextFactory contextFactory)
        {
            this.database = database;
            this.runtimeIdGenerator = runtimeIdGenerator;
            this.contextFactory = contextFactory;
        }

        public IRuntimeObject Create(string ownerId, IData data)
        {
            if (data is not ObjectData objectData)
                throw new NotImplementedException();

            var runtimeId = runtimeIdGenerator.Next();
            var runtimeData = objectData.Type switch
            {
                ObjectType.Creature or ObjectType.Spell => new RuntimeCardData
                {
                    DataId = data.Id,
                    Id = runtimeId,
                    OwnerId = ownerId,
                    Stats = objectData.Stats.Select(o => CreateRuntimeStatData(runtimeId, ownerId, o)).ToList()
                },
                _ => throw new NotImplementedException()
            };

            return Create(runtimeData);
        }

        public IRuntimeObject Create(IRuntimeData runtimeData)
        {
            if (runtimeData is not IRuntimeObjectData runtimeObjectData)
                throw new NotImplementedException();

            var objectData = database.Objects.Get(runtimeData.DataId);
            var eventSource = contextFactory.CreateEventsSource();
            var statsCollection = contextFactory.CreateStatsCollection(eventSource);
            var effectsCollection = contextFactory.CreateEffectsCollection(eventSource);

            return objectData.Type switch
            {
                ObjectType.Creature or ObjectType.Spell
                    => new RuntimeCard().Init(objectData, runtimeObjectData, statsCollection, effectsCollection, eventSource),
                _ => throw new NotImplementedException()
            };
        }

        private IRuntimeStatData CreateRuntimeStatData(int runtimeId, string ownerId, StatData statData)
        {
            return new RuntimeStatData
            {
                Id = runtimeId,
                DataId = statData.Id,
                OwnerId = ownerId,
                Base = statData.Base,
                Max = statData.Max,
                Value = statData.Value,
                Name = statData.Name
            };
        }
    }
}