using Client.Game.Abstractions.Collections.Queues;
using Shared.Abstractions.Game.Commands;
using Shared.Abstractions.Game.Context;
using Shared.Abstractions.Game.Events;
using Shared.Abstractions.Game.Runtime.Objects;
using Shared.Game.Context.EventProcessors;
using Shared.Game.Events.Context.Cards;
using Shared.Game.Events.Context.Commands;
using Shared.Game.Events.Context.Effects;
using Shared.Game.Events.Context.Objects;
using Shared.Game.Events.Context.Queue;
using Shared.Game.Events.Context.Stats;
using Shared.Game.Events.Output;
using Shared.Game.Utils;

namespace Client.Game.Context.EventProcessors
{
    public class PredictableObjectEventProcessor : ObjectEventProcessor
    {
        private readonly IContext context;
        private readonly IGameEventRollbackQueue rollbackQueue;
        private bool contextSubscribed;
        private string predictionId;

        public PredictableObjectEventProcessor(
            IContext context,
            IGameQueueCollector queueCollector,
            IGameEventRollbackQueue rollbackQueue) : base(queueCollector)
        {
            this.context = context;
            this.rollbackQueue = rollbackQueue;
        }

        private void SubscribeContext()
        {
            context.EventSource.Subscribe<BeforeCommandExecuteEvent>(ev => RegisterPrediction(ev.Command));
            context.EventSource.Subscribe<AfterGameQueueReleasedEvent>(_ => predictionId = null);
        }

        private void RegisterPrediction(ICommand command)
        {
            if (!string.IsNullOrWhiteSpace(predictionId) || command?.Model is null or {IsNested: true})
                return;

            predictionId = command.Model.PredictionId;
        }

        protected override void OnSubscribe(IRuntimeObjectBase runtimeObject)
        {
            var eventSource = runtimeObject.EventsSource;
            
            if (contextSubscribed)
            {
                contextSubscribed = true;
                SubscribeContext();
            }
            
            #region Objects

            eventSource.Subscribe<AfterObjectAddedEvent>(data =>
                RegisterRollback(new ObjectDeleted{RuntimeData = data.RuntimeObject.RuntimeData.Clone()}));

            eventSource.Subscribe<AfterObjectDeletedEvent>(data =>
                RegisterRollback(new AddedObject{RuntimeData = data.RuntimeObject.RuntimeData.Clone()}));
            
            eventSource.Subscribe<BeforeObjectStateChangeEvent>(data =>
                RegisterRollback(new ObjectStateChanged().Map(data.RuntimeObject.RuntimeData)));
            
            eventSource.Subscribe<BeforeCardPositionChangeEvent>(data =>
                RegisterRollback(new CardPositionChanged().Map(data.RuntimeObject.RuntimeData)));
            
            // TODO: execute hit effect
            // eventSource.Subscribe<AfterObjectHitEvent>(data =>
            //     queueCollector.Register(new HitObject{RuntimeData = data.RuntimeStat.RuntimeData.Clone()}));

            #endregion

            #region Effects

            eventSource.Subscribe<AfterEffectAddedEvent>(data =>
                RegisterRollback(new EffectDeleted{RuntimeData = data.RuntimeEffect.RuntimeData.Clone()})); 
            
            eventSource.Subscribe<AfterEffectDeletedEvent>(data =>
                RegisterRollback(new EffectAdded{RuntimeData = data.RuntimeEffect.RuntimeData.Clone()})); 
            
            eventSource.Subscribe<BeforeEffectChangeEvent>(data =>
                RegisterRollback(new EffectChanged{RuntimeData = data.RuntimeEffect.RuntimeData.Clone()})); 
            
            eventSource.Subscribe<BeforeEffectExecuteEvent>(data =>
                RegisterRollback(new EffectChanged{RuntimeData = data.RuntimeEffect.RuntimeData.Clone()})); 

            #endregion

            #region Stats

            eventSource.Subscribe<AfterStatAddedEvent>(data =>
                RegisterRollback(new StatDeleted{RuntimeData = data.RuntimeStat.RuntimeData.Clone()}));

            eventSource.Subscribe<AfterStatDeletedEvent>(data =>
                RegisterRollback(new StatAdded{RuntimeData = data.RuntimeStat.RuntimeData.Clone()}));
            
            eventSource.Subscribe<BeforeStatChangeEvent>(data =>
                RegisterRollback(new StatChanged{RuntimeData = data.RuntimeStat.RuntimeData.Clone()}));

            #endregion
            base.OnSubscribe(runtimeObject);
        }

        private void RegisterRollback(IGameEvent gameEvent)
        {
            gameEvent.PredictionId = predictionId;
            gameEvent.Rollback = true;
            gameEvent.Order = context.RuntimeOrderProvider.RuntimeData.NextOrder;
            rollbackQueue.Enqueue(gameEvent);
        }
    }
}