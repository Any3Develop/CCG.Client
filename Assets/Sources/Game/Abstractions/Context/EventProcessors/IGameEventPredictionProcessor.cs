using Shared.Abstractions.Game.Commands;

namespace Client.Game.Abstractions.Context.EventProcessors
{
    public interface IGameEventPredictionProcessor
    {
        void Execute(ICommandModel model);
    }
}