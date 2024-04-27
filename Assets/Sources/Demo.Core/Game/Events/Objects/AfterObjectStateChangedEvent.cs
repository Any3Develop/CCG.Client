using Demo.Core.Abstractions.Game.Runtime.Objects;

namespace Demo.Core.Game.Events.Objects
{
    public readonly struct AfterObjectStateChangedEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public AfterObjectStateChangedEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}