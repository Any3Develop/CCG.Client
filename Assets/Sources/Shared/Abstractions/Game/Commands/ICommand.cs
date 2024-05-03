namespace Shared.Abstractions.Game.Commands
{
    public interface ICommand
    {
        string ExecutorId { get; }
        ICommandModel Model { get; }
        void Execute();
    }

    public interface ICommand<out T> : ICommand where T : ICommandModel
    {
        new T Model { get; }
    }
}