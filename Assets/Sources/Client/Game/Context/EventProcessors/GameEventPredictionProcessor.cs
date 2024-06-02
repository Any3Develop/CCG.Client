using System;
using Client.Game.Abstractions.Collections.Queues;
using Client.Game.Abstractions.Context.EventProcessors;
using Client.Lobby.Runtime;
using Cysharp.Threading.Tasks;
using Shared.Abstractions.Game.Commands;
using Shared.Abstractions.Game.Context;
using Shared.Common.Logger;
using Shared.Game.Events.Context.Queue;

namespace Client.Game.Context.EventProcessors
{
    public class GameEventPredictionProcessor : IGameEventPredictionProcessor
    {
        private readonly IContext context;
        private readonly IGameEventPredictedQueue predictedQueue;
        private readonly IGameEventQueueLocalProcessor queueLocalProcessor;
        private readonly ICommandProcessor commandProcessor;

        public GameEventPredictionProcessor(
            IContext context,
            IGameEventPredictedQueue predictedQueue,
            IGameEventQueueLocalProcessor queueLocalProcessor,
            ICommandProcessor commandProcessor)
        {
            this.context = context;
            this.predictedQueue = predictedQueue;
            this.queueLocalProcessor = queueLocalProcessor;
            this.commandProcessor = commandProcessor;
        }

        public void Execute(ICommandModel model)
        {
            model.PredictionId = Guid.NewGuid().ToString();
            using var _ = context.EventSource.Subscribe<AfterGameQueueReleasedEvent>(data => predictedQueue.Enqueue(data.Queue));
            commandProcessor.Execute(User.Id, model);
            queueLocalProcessor.ProcessAsync(predictedQueue).Forget(SharedLogger.Error);
        }
    }
}