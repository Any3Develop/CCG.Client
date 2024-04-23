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
using Demo.Core.Game.RuntimeObjects.Effects;

namespace Demo.Core.Game.Factories
{
    public class RuntimeEffectFactory : IRuntimeFactory<IRuntimeEffect>
    {
        private readonly IDatabase database;
        private readonly IRuntimePool runtimePool;
        private readonly IRuntimeIdProvider runtimeIdProvider;

        public RuntimeEffectFactory(
            IDatabase database, 
            IRuntimePool runtimePool,
            IRuntimeIdProvider runtimeIdProvider)
        {
            this.database = database;
            this.runtimePool = runtimePool;
            this.runtimeIdProvider = runtimeIdProvider;
        }

        public IRuntimeEffect Create(int? runtimeId, string ownerId, IData data, bool notify = true)
        {
            if (data is not EffectData effectData)
                throw new InvalidCastException($"To create {nameof(IRuntimeEffect)} you should inject {nameof(EffectData)} instead {data?.GetType().Name ?? "Type.Undefined"}");

            runtimeId ??= runtimeIdProvider.Next();
            var runtimeData = effectData.Keyword switch // TODO: switch is for example, but instead switch it's going to be trough reflection & attribute with keyword
            {
                Keyword.None => new RuntimeEffectData
                {
                    DataId = effectData.Id,
                    Id = runtimeId.Value,
                    OwnerId = ownerId,
                    Lifetime = effectData.Lifetime,
                    Value = effectData.Value
                },
                _ => throw new NotImplementedException($"Unknown {nameof(Keyword)}: {effectData.Keyword}")
            };

            return Create(runtimeData, notify);
        }

        public IRuntimeEffect Create(IRuntimeData runtimeData, bool notify = true)
        {
            if (runtimeData is not IRuntimeEffectData runtimeEffectData)
                throw new InvalidCastException($"To create {nameof(IRuntimeEffect)} you should inject {nameof(IRuntimeEffectData)} instead {runtimeData?.GetType().Name ?? "Type.Undefined"}");
            
            if (!runtimePool.TryGet(runtimeEffectData.EffectOwnerId, out IRuntimeObject runtimeEffectOwnerObject))
                throw new NullReferenceException($"{nameof(IRuntimeObject)} with id {runtimeEffectData.EffectOwnerId}, not found in {nameof(IRuntimePool)}");

            if (!database.Effects.TryGet(runtimeEffectData.DataId, out var effectData))
                throw new NullReferenceException($"{nameof(EffectData)} with id {runtimeEffectData.DataId}, not found in {nameof(IDataCollection<EffectData>)}");
            
            var runtimeEffect = effectData.Keyword switch // TODO: switch is for example, but instead switch it's going to be trough reflection & attribute with keyword
            {
                Keyword.None => new RuntimeMockEffect(),
                _ => throw new NotImplementedException($"Unknown {nameof(Keyword)}: {effectData.Keyword}")
            };
            
            runtimePool.Add(runtimeEffect, notify);
            runtimeEffect.Init(effectData, runtimeEffectData, runtimeEffectOwnerObject.EventsSource);
            return runtimeEffect;
        }
    }
}