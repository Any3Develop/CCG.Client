using System;
using Demo.Core.Abstractions.Common.Collections;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.Context;
using Demo.Core.Abstractions.Game.Factories;
using Demo.Core.Abstractions.Game.Runtime.Common;
using Demo.Core.Abstractions.Game.Runtime.Data;
using Demo.Core.Abstractions.Game.Runtime.Effects;
using Demo.Core.Game.Data;
using Demo.Core.Game.Enums;
using Demo.Core.Game.Runtime.Data;
using Demo.Core.Game.Runtime.Effects;

namespace Demo.Core.Game.Factories
{
    public class RuntimeEffectFactory : IRuntimeEffectFactory
    {
        private readonly IDatabase database;
        private readonly IRuntimePool runtimePool;
        private readonly IRuntimeIdProvider runtimeIdProvider;
        private readonly ITypeCollection<Keyword> keywordTypeCollection;

        public RuntimeEffectFactory(
            IDatabase database, 
            IRuntimePool runtimePool,
            IRuntimeIdProvider runtimeIdProvider,
            ITypeCollection<Keyword> keywordTypeCollection)
        {
            this.database = database;
            this.runtimePool = runtimePool;
            this.runtimeIdProvider = runtimeIdProvider;
            this.keywordTypeCollection = keywordTypeCollection;
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
            if (!runtimePool.TryGet(runtimeData.EffectOwnerId, out IRuntimeObject runtimeEffectOwnerObject))
                throw new NullReferenceException($"{nameof(IRuntimeObject)} with id {runtimeData.EffectOwnerId}, not found in {nameof(IRuntimePool)}");

            if (runtimeEffectOwnerObject.EffectsCollection.TryGet(runtimeData.Id, out var runtimeEffect))
                return runtimeEffect.Sync(runtimeData);
            
            if (!database.Effects.TryGet(runtimeData.DataId, out var data))
                throw new NullReferenceException($"{nameof(EffectData)} with id {runtimeData.DataId}, not found in {nameof(IDataCollection<EffectData>)}");
            
            runtimeEffect = CreateEffectInstance(data.Keyword).Init(data, runtimeData, runtimeEffectOwnerObject.EventsSource);
            runtimeEffectOwnerObject.EffectsCollection.Add(runtimeEffect);
            
            return runtimeEffect;
        }

        private RuntimeEffect CreateEffectInstance(Keyword keyword)
        {
            if (!keywordTypeCollection.TryGet(keyword, out var effectType))
                throw new NullReferenceException($"{nameof(Type)} with {nameof(Keyword)} {keyword}, not found in {nameof(ITypeCollection<Keyword>)}");
            
            var constructorInfo = effectType.GetConstructor(Type.EmptyTypes);

            if (constructorInfo == null)
                throw new NullReferenceException($"{effectType.Name} with {nameof(Keyword)} {keyword}, default constructor not found.");
          
            return (RuntimeEffect)constructorInfo.Invoke(Array.Empty<object>());
        }
    }
}