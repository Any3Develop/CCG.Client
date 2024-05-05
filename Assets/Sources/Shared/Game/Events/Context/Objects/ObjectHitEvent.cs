using Shared.Abstractions.Game.Runtime.Objects;

namespace Shared.Game.Events.Context.Objects
{
    public readonly struct ObjectHitEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public ObjectHitEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}