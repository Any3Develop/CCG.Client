using Shared.Abstractions.Game.Runtime.Objects;

namespace Shared.Game.Events.Context.Objects
{
    public readonly struct BeforeDeletedObjectEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public BeforeDeletedObjectEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}