using Shared.Abstractions.Game.Commands;

namespace Shared.Abstractions.Game.Factories
{
    public interface ICommandFactory
    {
        ICommand Create<T>(string executorId, ICommandModel model) where T : ICommand;
        ICommand Create(string executorId, string command, ICommandModel model);
    }
}