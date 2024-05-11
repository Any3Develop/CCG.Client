using Shared.Abstractions.Game.Runtime.Objects;

namespace Shared.Game.Events.Context.Objects
{
    public readonly struct AfterObjectAddedEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public AfterObjectAddedEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}