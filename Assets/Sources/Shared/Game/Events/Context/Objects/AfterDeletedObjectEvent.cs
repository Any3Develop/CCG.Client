using Shared.Abstractions.Game.Runtime.Objects;

namespace Shared.Game.Events.Context.Objects
{
    public readonly struct AfterDeletedObjectEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public AfterDeletedObjectEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}