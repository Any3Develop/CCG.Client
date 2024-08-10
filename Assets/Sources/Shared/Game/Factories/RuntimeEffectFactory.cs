using System;
using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context;
using Shared.Abstractions.Game.Context.Providers;
using Shared.Abstractions.Game.Factories;
using Shared.Abstractions.Game.Runtime.Data;
using Shared.Abstractions.Game.Runtime.Effects;
using Shared.Abstractions.Game.Runtime.Objects;
using Shared.Game.Data;
using Shared.Game.Data.Enums;
using Shared.Game.Runtime.Data;
using Shared.Game.Runtime.Effects;

namespace Shared.Game.Factories
{
    public class RuntimeEffectFactory : IRuntimeEffectFactory
    {
        private readonly IDatabase database;
        private readonly IObjectsCollection objectsCollection;
        private readonly IRuntimeIdProvider runtimeIdProvider;
        private readonly ITypeCollection<LogicId> logicTypeCollection;

        public RuntimeEffectFactory(
            IDatabase database, 
            IObjectsCollection objectsCollection,
            IRuntimeIdProvider runtimeIdProvider,
            ITypeCollection<LogicId> logicTypeCollection)
        {
            this.database = database;
            this.objectsCollection = objectsCollection;
            this.runtimeIdProvider = runtimeIdProvider;
            this.logicTypeCollection = logicTypeCollection;
        }

        public IRuntimeEffectData Create(int? runtimeId, string ownerId, string dataId, bool notify = true)
        {           
            if (!database.Effects.TryGet(dataId, out var data))
                throw new NullReferenceException($"{nameof(EffectData)} with id {dataId}, not found in {nameof(IDataCollection<EffectData>)}");
            
            return new RuntimeEffectData // TODO: use keyword to create specified runtime data
            {
                DataId = data.Id,
                Id = runtimeId ?? runtimeIdProvider.Next(),
                OwnerId = ownerId,
                Lifetime = data.Lifetime,
                Value = data.Value
            };
        }

        public IRuntimeEffect Create(IRuntimeEffectData runtimeData, bool notify = true)
        {
            if (!objectsCollection.TryGet(runtimeData.EffectOwnerId, out var runtimeEffectOwnerObject))
                throw new NullReferenceException($"{nameof(IRuntimeObject)} with id {runtimeData.EffectOwnerId}, not found in {nameof(IObjectsCollection)}");

            if (runtimeEffectOwnerObject.EffectsCollection.TryGet(runtimeData.Id, out var runtimeEffect))
                return runtimeEffect.Sync(runtimeData, notify);
            
            if (!database.Effects.TryGet(runtimeData.DataId, out var data))
                throw new NullReferenceException($"{nameof(EffectData)} with id {runtimeData.DataId}, not found in {nameof(IDataCollection<EffectData>)}");
            
            runtimeEffect = CreateEffectInstance(data.LogicId).Init(data, runtimeData, runtimeEffectOwnerObject.EventPublisher, runtimeEffectOwnerObject.EventsSource);
            runtimeEffectOwnerObject.EffectsCollection.Add(runtimeEffect, notify);
            
            return runtimeEffect;
        }

        private RuntimeEffect CreateEffectInstance(LogicId logicId)
        {
            if (!logicTypeCollection.TryGet(logicId, out var effectType))
                throw new NullReferenceException($"{nameof(Type)} with {nameof(LogicId)} {logicId}, not found in {nameof(ITypeCollection<LogicId>)}");
            
            var constructorInfo = effectType.GetConstructor(Type.EmptyTypes);

            if (constructorInfo == null)
                throw new NullReferenceException($"{effectType.Name} with {nameof(LogicId)} {logicId}, default constructor not found.");
          
            return (RuntimeEffect)constructorInfo.Invoke(Array.Empty<object>());
        }
    }
}