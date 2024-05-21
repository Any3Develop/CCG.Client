using Shared.Abstractions.Game.Commands;

namespace Shared.Abstractions.Game.Factories
{
    public interface ICommandFactory
    {
        ICommand Create(string executorId, ICommandModel model);
    }
}