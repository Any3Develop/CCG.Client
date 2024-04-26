using Demo.Core.Abstractions.Game.Runtime.Common;

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