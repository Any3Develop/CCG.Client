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

            eventSource.Subscribe<AfterObjectAddedEvent>(data =>
                queueCollector.Register(new AddedObject{RuntimeData = data.RuntimeObject.RuntimeData.Clone()}));

            eventSource.Subscribe<AfterObjectDeletedEvent>(data =>
                queueCollector.Register(new ObjectDeleted{RuntimeData = data.RuntimeObject.RuntimeData.Clone()}));
            
            eventSource.Subscribe<AfterObjectStateChangedEvent>(data =>
                queueCollector.Register(new ObjectStateChanged().Map(data.RuntimeObject.RuntimeData)));
            
            eventSource.Subscribe<AfterCardPositionChangedEvent>(data =>
                queueCollector.Register(new CardPositionChanged().Map(data.RuntimeObject.RuntimeData)));
            
            // TODO: execute hit effect
            // eventSource.Subscribe<AfterObjectHitEvent>(data =>
            //     queueCollector.Register(new HitObject{RuntimeData = data.RuntimeStat.RuntimeData.Clone()}));

            #endregion

            #region Effects

            eventSource.Subscribe<AfterEffectAddedEvent>(data =>
                queueCollector.Register(new EffectAdded{RuntimeData = data.RuntimeEffect.RuntimeData.Clone()})); 
            
            eventSource.Subscribe<AfterEffectDeletedEvent>(data =>
                queueCollector.Register(new EffectDeleted{RuntimeData = data.RuntimeEffect.RuntimeData.Clone()})); 
            
            eventSource.Subscribe<AfterEffectChangedEvent>(data =>
                queueCollector.Register(new EffectChanged{RuntimeData = data.RuntimeEffect.RuntimeData.Clone()})); 
            
            eventSource.Subscribe<BeforeEffectExecuteEvent>(data =>
                queueCollector.Register(new EffectStarted{RuntimeData = data.RuntimeEffect.RuntimeData.Clone()})); 
                        
            eventSource.Subscribe<AfterEffectExecutedEvent>(data =>
                queueCollector.Register(new EffectEnded{RuntimeData = data.RuntimeEffect.RuntimeData.Clone()}));  

            #endregion

            #region Stats

            eventSource.Subscribe<AfterStatAddedEvent>(data =>
                queueCollector.Register(new StatAdded{RuntimeData = data.RuntimeStat.RuntimeData.Clone()}));

            eventSource.Subscribe<AfterStatDeletedEvent>(data =>
                queueCollector.Register(new StatDeleted{RuntimeData = data.RuntimeStat.RuntimeData.Clone()}));
            
            eventSource.Subscribe<AfterStatChangedEvent>(data =>
                queueCollector.Register(new StatChanged{RuntimeData = data.RuntimeStat.RuntimeData.Clone()}));

            #endregion

        }
    }
}