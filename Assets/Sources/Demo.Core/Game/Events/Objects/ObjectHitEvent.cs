using Demo.Core.Abstractions.Game.Runtime.Objects;

namespace Demo.Core.Game.Events.Objects
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