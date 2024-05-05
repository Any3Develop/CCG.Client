using Shared.Abstractions.Game.Runtime.Objects;

namespace Shared.Game.Events.Context.Objects
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