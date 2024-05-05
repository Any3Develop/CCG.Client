using Client.Game.Abstractions.Context.Logic.Prediction;
using Client.Game.Abstractions.Context.Logic.Queue;
using Shared.Abstractions.Game.Commands;

namespace Client.Game.Context.Logic.Prediction
{
    public class PredictionProcessor : IPredictionProcessor
    {
        private readonly IPredictionQueue predictionQueue;
        private readonly IReconciliationQueue reconciliationQueue;
        private readonly IGameEventQueueLocalProcessor queueLocalProcessor;


        public PredictionProcessor(
            IPredictionQueue predictionQueue,
            IReconciliationQueue reconciliationQueue,
            IGameEventQueueLocalProcessor queueLocalProcessor)
        {
            this.predictionQueue = predictionQueue;
            this.reconciliationQueue = reconciliationQueue;
            this.queueLocalProcessor = queueLocalProcessor;
        }

        public void Execute<TCommand>(ICommandModel model) where TCommand : ICommand
        {
            throw new System.NotImplementedException();
        }
    }
}