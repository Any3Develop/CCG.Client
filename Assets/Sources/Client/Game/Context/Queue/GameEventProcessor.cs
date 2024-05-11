using System;
using Client.Game.Abstractions.Context.Queue;
using Shared.Abstractions.Game.Context;
using Shared.Abstractions.Game.Events;
using Shared.Abstractions.Game.Runtime.Cards;
using Shared.Game.Events.Output;

namespace Client.Game.Context.Queue
{
    public class GameEventProcessor : IGameEventProcessor
    {
        private readonly IContext context;

        public GameEventProcessor(IContext context)
        {
            this.context = context;
        }

        public void Process(IGameEvent gameEvent)
        {
            switch (gameEvent)
            {
                case EffectAdded addedObjectEffect:
                {
                    context.EffectFactory.Create(addedObjectEffect.RuntimeData, false);
                    return;
                }

                case StatAdded addedObjectStat:
                {
                    context.StatFactory.Create(addedObjectStat.RuntimeData, false);
                    return;
                }

                case EffectChanged changedObjectEffect:
                {
                    if (!context.ObjectsCollection.TryGet(changedObjectEffect.RuntimeData.EffectOwnerId, out var runtimeObject)
                        || !runtimeObject.EffectsCollection.TryGet(changedObjectEffect.RuntimeData.Id, out var runtimeEffect))
                        return;

                    runtimeEffect.Sync(changedObjectEffect.RuntimeData, false);
                    return;
                }
                
                case CardPositionChanged changedObjectPosition:
                {
                    if (!context.ObjectsCollection.TryGet(changedObjectPosition.Id, out IRuntimeCard runtimeCard))
                        return;
                    
                    runtimeCard.SetPosition(changedObjectPosition.Position, false);
                    return;
                }
                
                case StatChanged changedObjectStat:
                {
                    if (!context.ObjectsCollection.TryGet(changedObjectStat.RuntimeData.RuntimeOwnerId, out var runtimeObject)
                        || !runtimeObject.StatsCollection.TryGet(changedObjectStat.RuntimeData.Id, out var runtimeStat))
                        return;
                    
                    runtimeStat.Sync(changedObjectStat.RuntimeData, false);
                    return;
                }
                
                case ObjectStateChanged changedObjectState:
                {
                    if (!context.ObjectsCollection.TryGet(changedObjectState.Id, out var runtimeObject))
                        return;
                    
                    runtimeObject.SetState(changedObjectState.State, false);
                    return;
                }
                
                case ObjectDeleted deletedObject:
                {
                    if (!context.ObjectsCollection.TryGet(deletedObject.RuntimeData.Id, out var runtimeObject))
                        return;
                    
                    context.ObjectsCollection.Remove(runtimeObject, false);
                    runtimeObject.Dispose();
                    return;
                }
                
                case EffectDeleted deletedObjectEffect:
                {
                    if (!context.ObjectsCollection.TryGet(deletedObjectEffect.RuntimeData.EffectOwnerId, out var runtimeObject)
                        || !runtimeObject.EffectsCollection.TryGet(deletedObjectEffect.RuntimeData.Id, out var runtimeEffect))
                        return;

                    runtimeObject.EffectsCollection.Remove(runtimeEffect, false);
                    runtimeEffect.Dispose();
                    return;
                }
                
                case StatDeleted deletedObjectStat:
                {
                    if (!context.ObjectsCollection.TryGet(deletedObjectStat.RuntimeData.RuntimeOwnerId, out var runtimeObject)
                        || !runtimeObject.StatsCollection.TryGet(deletedObjectStat.RuntimeData.Id, out var runtimeStat))
                        return;

                    runtimeObject.StatsCollection.Remove(runtimeStat, false);
                    runtimeStat.Dispose();
                    return;
                }
                
                case EffectEnded endedObjectEffect:
                {
                    if (!context.ObjectsCollection.TryGet(endedObjectEffect.RuntimeData.EffectOwnerId, out var runtimeObject)
                        || !runtimeObject.EffectsCollection.TryGet(endedObjectEffect.RuntimeData.Id, out var runtimeEffect))
                        return;
                    
                    runtimeEffect.Sync(endedObjectEffect.RuntimeData, false);
                    return;
                }
                
                case AddedObject addedObject:
                {
                    context.ObjectFactory.Create(addedObject.RuntimeData, false);
                    return;
                }
                
                case EffectStarted startObjectEffect:
                {
                    if (!context.ObjectsCollection.TryGet(startObjectEffect.RuntimeData.EffectOwnerId, out var runtimeObject)
                        || !runtimeObject.EffectsCollection.TryGet(startObjectEffect.RuntimeData.Id, out var runtimeEffect))
                        return;
                    
                    runtimeEffect.Sync(startObjectEffect.RuntimeData, false);
                    return;
                }
                
                case SyncRuntimeId syncRuntimeId:
                {
                    context.RuntimeIdProvider.Sync(syncRuntimeId.RuntimeData);
                    return;
                }
                
                case SyncRuntimeOrder syncRuntimeOrder:
                {
                    context.RuntimeOrderProvider.Sync(syncRuntimeOrder.RuntimeData);
                    return;
                }
                
                case SyncRuntimeRandom syncRuntimeRandom: 
                    context.RuntimeRandomProvider.Sync(syncRuntimeRandom.RuntimeData); break;

                default: throw new NotImplementedException($"{GetType().Name}, Unknown {nameof(IGameEvent)} '{gameEvent?.GetType().FullName}'");
            }
        }
    }
}