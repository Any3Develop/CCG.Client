using Shared.Abstractions.Game.Commands;

namespace Shared.Game.Events.Context
{
    public readonly struct AfterCommandExecutedEvent
    {
        public ICommand Command { get; }
        public AfterCommandExecutedEvent(ICommand command)
        {
            Command = command;
        }
    }
}