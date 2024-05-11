using Shared.Abstractions.Game.Runtime.Objects;

namespace Shared.Game.Events.Context.Objects
{
    public readonly struct AfterObjectDeletedEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public AfterObjectDeletedEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}