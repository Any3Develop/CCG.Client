using Shared.Abstractions.Game.Commands;

namespace Client.Game.Abstractions.Context.EventProcessors
{
    public interface IGameEventQueuePredictionProcessor
    {
        void Execute<TCommand>(ICommandModel model) where TCommand : ICommand;
    }
}