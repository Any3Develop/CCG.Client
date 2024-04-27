namespace Demo.Core.Abstractions.Game.Commands
{
    public interface ICommand
    {
        ICommandModel Model { get; }
        void Execute();
    }

    public interface ICommand<out T> : ICommand where T : ICommandModel
    {
        new T Model { get; }
    }
}