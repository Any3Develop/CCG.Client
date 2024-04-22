using Demo.Core.Abstractions.Game.RuntimeObjects;

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