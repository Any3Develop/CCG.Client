using Shared.Abstractions.Game.Commands;

namespace Shared.Abstractions.Game.Context
{
    public interface ICommandProcessor
    {
        void Execute(string executorId, ICommandModel model);
    }
}