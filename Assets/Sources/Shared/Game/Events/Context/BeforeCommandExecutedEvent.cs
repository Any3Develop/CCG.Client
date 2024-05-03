using Shared.Abstractions.Game.Commands;

namespace Shared.Game.Events.Context
{
    public readonly struct BeforeCommandExecutedEvent
    {
        public ICommand Command { get; }
        public BeforeCommandExecutedEvent(ICommand command)
        {
            Command = command;
        }
    }
}