using Shared.Abstractions.Game.Runtime.Objects;

namespace Shared.Game.Events.Context.Objects
{
    public readonly struct AfterObjectChangedEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public AfterObjectChangedEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}