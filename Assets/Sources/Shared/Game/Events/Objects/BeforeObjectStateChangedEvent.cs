using Shared.Abstractions.Game.Runtime.Objects;

namespace Shared.Game.Events.Objects
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