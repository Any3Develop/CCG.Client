using Shared.Abstractions.Game.Runtime.Objects;

namespace Shared.Game.Events.Context.Objects
{
    public readonly struct AfterAddedObjectEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public AfterAddedObjectEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}