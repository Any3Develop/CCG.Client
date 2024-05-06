using Shared.Abstractions.Game.Commands;

namespace Shared.Abstractions.Game.Context
{
    public interface ICommandProcessor
    {
        void Execute<TCommand>(string executorId, ICommandModel model) where TCommand : ICommand;
        void Execute(string executorId, string command, ICommandModel model);
    }
}