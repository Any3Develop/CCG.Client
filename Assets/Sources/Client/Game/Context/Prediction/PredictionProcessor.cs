using Client.Game.Abstractions.Collections.Queues;
using Client.Game.Abstractions.Context.Prediction;
using Client.Game.Abstractions.Context.Queue;
using Shared.Abstractions.Game.Commands;

namespace Client.Game.Context.Prediction
{
    public class PredictionProcessor : IPredictionProcessor
    {
        private readonly IGameEventPredictionQueue gameEventPredictionQueue;
        private readonly IGameEventRollbackQueue gameEventRollbackQueue;
        private readonly IGameEventQueueProcessor queueProcessor;


        public PredictionProcessor(
            IGameEventPredictionQueue gameEventPredictionQueue,
            IGameEventRollbackQueue gameEventRollbackQueue,
            IGameEventQueueProcessor queueProcessor)
        {
            this.gameEventPredictionQueue = gameEventPredictionQueue;
            this.gameEventRollbackQueue = gameEventRollbackQueue;
            this.queueProcessor = queueProcessor;
        }

        public void Execute<TCommand>(ICommandModel model) where TCommand : ICommand
        {
            throw new System.NotImplementedException();
        }
    }
}