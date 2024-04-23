using System;
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
        private readonly IRuntimePool runtimePool;
        private readonly IRuntimeIdProvider runtimeIdProvider;
        private readonly IRuntimeFactory<IRuntimeStat> runtimeStatFactory;
        private readonly IContextFactory contextFactory;

        public RuntimeObjectFactory(
            IDatabase database,
            IRuntimePool runtimePool,
            IRuntimeIdProvider runtimeIdProvider,
            IRuntimeFactory<IRuntimeStat> runtimeStatFactory,
            IContextFactory contextFactory)
        {
            this.database = database;
            this.runtimePool = runtimePool;
            this.runtimeIdProvider = runtimeIdProvider;
            this.runtimeStatFactory = runtimeStatFactory;
            this.contextFactory = contextFactory;
        }

        public IRuntimeObject Create(int? runtimeId, string ownerId, IData data, bool notify = true)
        {
            if (data is not ObjectData objectData)
                throw new InvalidCastException($"To create {nameof(IRuntimeObject)} you should inject {nameof(ObjectData)} instead {data?.GetType().Name ?? "Type.Undefined"}");

            runtimeId ??= runtimeIdProvider.Next();
            var runtimeData = objectData.Type switch
            {
                ObjectType.Creature or ObjectType.Spell => new RuntimeCardData
                {
                    DataId = data.Id,
                    Id = runtimeId.Value,
                    OwnerId = ownerId
                },
                _ => throw new NotImplementedException()
            };

            return Create(runtimeData, notify);
        }

        public IRuntimeObject Create(IRuntimeData runtimeData, bool notify = true)
        {
            if (runtimeData is not IRuntimeObjectData runtimeObjectData)
                throw new InvalidCastException($"To create {nameof(IRuntimeObject)} you should inject {nameof(IRuntimeObjectData)} instead {runtimeData?.GetType().Name ?? "Type.Undefined"}");

            var objectData = database.Objects.Get(runtimeData.DataId);
            var eventSource = contextFactory.CreateEventsSource();
            var statsCollection = contextFactory.CreateStatsCollection(eventSource);
            var effectsCollection = contextFactory.CreateEffectsCollection(eventSource);
            var runtimeObject = objectData.Type switch
            {
                ObjectType.Creature or ObjectType.Spell
                    => new RuntimeCard(),
                _ => throw new NotImplementedException($"Unknown {nameof(ObjectType)}: {objectData.Type}")
            };
            
            runtimePool.Add(runtimeObject, notify);
            runtimeObject.Init(objectData, runtimeObjectData, statsCollection, effectsCollection, eventSource);
            
            foreach (var statData in objectData.Stats)
                runtimeStatFactory.Create(runtimeData.Id, runtimeData.OwnerId, statData, notify);

            return runtimeObject;
        }
    }
}