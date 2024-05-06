using Shared.Abstractions.Game.Context;
using Shared.Abstractions.Game.Context.EventProcessors;
using Shared.Abstractions.Game.Runtime.Objects;
using Shared.Game.Events.Context.Cards;
using Shared.Game.Events.Context.Effects;
using Shared.Game.Events.Context.Objects;
using Shared.Game.Events.Context.Stats;
using Shared.Game.Events.Output;
using Shared.Game.Utils;

namespace Shared.Game.Context.EventProcessors
{
    public class ObjectEventProcessor : IObjectEventProcessor
    {
        private readonly IGameQueueCollector queueCollector;

        public ObjectEventProcessor(IGameQueueCollector queueCollector)
        {
            this.queueCollector = queueCollector;
        }

        public void Subscribe(IRuntimeObjectBase runtimeObject)
        {
            OnSubscribe(runtimeObject);
        }

        protected virtual void OnSubscribe(IRuntimeObjectBase runtimeObject)
        {
            var eventSource = runtimeObject.EventsSource;

            #region Object

            eventSource.Subscribe<AddedObjectEvent>(data =>
                queueCollector.Register(new AddedObject{RuntimeData = data.RuntimeObject.RuntimeData.Clone()}));

            eventSource.Subscribe<DeletedObjectEvent>(data =>
                queueCollector.Register(new DeletedObject{RuntimeData = data.RuntimeObject.RuntimeData.Clone()}));
            
            eventSource.Subscribe<AfterObjectStateChangedEvent>(data =>
                queueCollector.Register(new ChangedObjectState().Map(data.RuntimeObject.RuntimeData)));
            
            eventSource.Subscribe<AfterCardPositionChangedEvent>(data =>
                queueCollector.Register(new ChangedObjectPosition().Map(data.RuntimeObject.RuntimeData)));
            
            eventSource.Subscribe<AfterObjectHitEvent>(data =>
                queueCollector.Register(new HitObject{RuntimeData = data.RuntimeStat.RuntimeData.Clone()}));

            #endregion

            #region Effects

            eventSource.Subscribe<AfterEffectAddedEvent>(data =>
                queueCollector.Register(new AddedObjectEffect{RuntimeData = data.RuntimeEffect.RuntimeData.Clone()})); 
            
            eventSource.Subscribe<AfterEffectDeletedEvent>(data =>
                queueCollector.Register(new DeletedObjectEffect{RuntimeData = data.RuntimeEffect.RuntimeData.Clone()})); 
            
            eventSource.Subscribe<AfterEffectChangedEvent>(data =>
                queueCollector.Register(new ChangedObjectEffect{RuntimeData = data.RuntimeEffect.RuntimeData.Clone()})); 
            
            eventSource.Subscribe<BeforeEffectExecutedEvent>(data =>
                queueCollector.Register(new StartObjectEffect{RuntimeData = data.RuntimeEffect.RuntimeData.Clone()})); 
                        
            eventSource.Subscribe<AfterEffectExecutedEvent>(data =>
                queueCollector.Register(new EndedObjectEffect{RuntimeData = data.RuntimeEffect.RuntimeData.Clone()}));  

            #endregion

            #region Stats

            eventSource.Subscribe<AddedObjectStatEvent>(data =>
                queueCollector.Register(new AddedObjectStat{RuntimeData = data.RuntimeStat.RuntimeData.Clone()}));

            eventSource.Subscribe<DeletedObjectStatEvent>(data =>
                queueCollector.Register(new DeletedObjectStat{RuntimeData = data.RuntimeStat.RuntimeData.Clone()}));
            
            eventSource.Subscribe<AfterStatChangedEvent>(data =>
                queueCollector.Register(new ChangedObjectStat{RuntimeData = data.RuntimeStat.RuntimeData.Clone()}));

            #endregion

        }
    }
}