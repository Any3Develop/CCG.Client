using Shared.Abstractions.Game.Runtime.Objects;

namespace Shared.Game.Events.Context.Objects
{
    public readonly struct BeforeObjectStateChangeEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public BeforeObjectStateChangeEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}