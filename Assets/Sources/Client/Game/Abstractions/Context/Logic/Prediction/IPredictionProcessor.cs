using Shared.Abstractions.Game.Commands;

namespace Client.Game.Abstractions.Context.Logic.Prediction
{
    public interface IPredictionProcessor
    {
        void Execute<TCommand>(ICommandModel model) where TCommand : ICommand;
    }
}