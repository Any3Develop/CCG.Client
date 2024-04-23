using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Game.Events.Objects
{
    public readonly struct BeforeObjectStateChangedEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public BeforeObjectStateChangedEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}