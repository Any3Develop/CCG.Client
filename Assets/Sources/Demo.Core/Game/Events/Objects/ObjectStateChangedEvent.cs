using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Game.Events.Objects
{
    public readonly struct ObjectStateChangedEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public ObjectStateChangedEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}