using Shared.Abstractions.Game.Runtime.Objects;

namespace Shared.Game.Events.Context.Objects
{
    public readonly struct BeforeAddedObjectEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public BeforeAddedObjectEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}