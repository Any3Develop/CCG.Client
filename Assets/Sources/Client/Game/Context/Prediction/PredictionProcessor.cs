using System.Collections.Generic;
using Client.Game.Abstractions.Collections.Queues;
using Client.Game.Abstractions.Context.Prediction;
using Client.Game.Abstractions.Context.Queue;
using Client.Lobby.Runtime;
using Shared.Abstractions.Game.Commands;
using Shared.Abstractions.Game.Context;
using Shared.Abstractions.Game.Events;
using Shared.Game.Events.Context.Queue;

namespace Client.Game.Context.Prediction
{
    public class PredictionProcessor : IPredictionProcessor
    {
        private readonly IContext context;
        private readonly IGameEventRemoteQueue remoteQueue;
        private readonly IGameEventPredictedQueue predictedQueue;
        private readonly IGameEventRollbackQueue rollbackQueue;
        private readonly IGameEventQueueProcessor queueProcessor;
        private readonly ICommandProcessor commandProcessor;


        public PredictionProcessor(
            IContext context,
            IGameEventRemoteQueue remoteQueue,
            IGameEventPredictedQueue predictedQueue,
            IGameEventRollbackQueue rollbackQueue,
            IGameEventQueueProcessor queueProcessor,
            ICommandProcessor commandProcessor)
        {
            this.context = context;
            this.remoteQueue = remoteQueue;
            this.predictedQueue = predictedQueue;
            this.rollbackQueue = rollbackQueue;
            this.queueProcessor = queueProcessor;
            this.commandProcessor = commandProcessor;
        }

        public void Execute<TCommand>(ICommandModel model) where TCommand : ICommand
        {
            var subscription = context.EventSource.Subscribe<AfterGameQueueReleasedEvent>(data => predictedQueue.Enqueue(data.Queue));
            commandProcessor.Execute<TCommand>(User.Id, model);
            subscription.Dispose();
            queueProcessor.Register(predictedQueue);
            Reconciliation();
        }

        public void ProcessRemoteQueue(IEnumerable<IGameEvent> queue)
        {
            remoteQueue.Enqueue(queue);
            Reconciliation();
        }

        private void Reconciliation()
        {
            if (Compare())
                return;

            // TODO: Reconciliation
        }

        private bool Compare()
        {
            // TODO: Compare remote & predicted queue
            return false;
        }
    }
}