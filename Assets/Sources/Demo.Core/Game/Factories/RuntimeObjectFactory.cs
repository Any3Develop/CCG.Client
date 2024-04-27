﻿using System;
using System.Linq;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.Context;
using Demo.Core.Abstractions.Game.Factories;
using Demo.Core.Abstractions.Game.Runtime.Data;
using Demo.Core.Abstractions.Game.Runtime.Objects;
using Demo.Core.Game.Data;
using Demo.Core.Game.Data.Enums;
using Demo.Core.Game.Runtime.Cards;
using Demo.Core.Game.Runtime.Data;

namespace Demo.Core.Game.Factories
{
    public class RuntimeObjectFactory : IRuntimeObjectFactory
    {
        private readonly IDatabase database;
        private readonly IObjectsCollection objectsCollection;
        private readonly IRuntimeIdProvider runtimeIdProvider;
        private readonly IRuntimeStatFactory runtimeStatFactory;
        private readonly IContextFactory contextFactory;

        public RuntimeObjectFactory(
            IDatabase database,
            IObjectsCollection objectsCollection,
            IRuntimeIdProvider runtimeIdProvider,
            IRuntimeStatFactory runtimeStatFactory,
            IContextFactory contextFactory)
        {
            this.database = database;
            this.objectsCollection = objectsCollection;
            this.runtimeIdProvider = runtimeIdProvider;
            this.runtimeStatFactory = runtimeStatFactory;
            this.contextFactory = contextFactory;
        }

        public IRuntimeObjectData Create(int? runtimeId, string ownerId, string dataId, bool notify = true)
        {
            if (!database.Objects.TryGet(dataId, out var data))
                throw new NullReferenceException($"{nameof(ObjectData)} with id {dataId}, not found in {nameof(IDataCollection<ObjectData>)}");

            runtimeId ??= runtimeIdProvider.Next();
            return data.Type switch
            {
                ObjectType.Creature or ObjectType.Spell => new RuntimeCardData
                {
                    DataId = data.Id,
                    Id = runtimeId.Value,
                    OwnerId = ownerId,
                    Stats = data.StatIds.Select(id => runtimeStatFactory.Create(runtimeId.Value, ownerId, id, notify)).ToList(),
                },
                _ => throw new NotImplementedException($"Unknown {nameof(ObjectType)}: {data.Type}")
            };
        }
        
        public IRuntimeObject Create(IRuntimeObjectData runtimeData, bool notify = true)
        {
            if (objectsCollection.TryGet(runtimeData.Id, out var runtimeObject))
                return runtimeObject.Sync(runtimeData);
            
            if (!database.Objects.TryGet(runtimeData.DataId, out var data))
                throw new NullReferenceException($"{nameof(ObjectData)} with id {runtimeData.DataId}, not found in {nameof(IDataCollection<ObjectData>)}");
            
            var eventSource = contextFactory.CreateEventsSource();
            var statsCollection = contextFactory.CreateStatsCollection(eventSource);
            var effectsCollection = contextFactory.CreateEffectsCollection(eventSource);
            runtimeObject = data.Type switch
            {
                ObjectType.Creature => new RuntimeCardCreature().Init(data, runtimeData, statsCollection, effectsCollection, eventSource),
                ObjectType.Spell => new RuntimeCardSpell().Init(data, runtimeData, statsCollection, effectsCollection, eventSource),
                _ => throw new NotImplementedException($"Unknown {nameof(ObjectType)}: {data.Type}")
            };
            
            objectsCollection.Add(runtimeObject, notify);
            
            foreach (var runtimeStatData in runtimeData.Stats)
                runtimeStatFactory.Create(runtimeStatData, notify);
            
            return runtimeObject;
        }
    }
}