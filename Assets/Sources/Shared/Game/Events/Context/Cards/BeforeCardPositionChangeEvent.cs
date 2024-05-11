using Shared.Abstractions.Game.Runtime.Cards;

namespace Shared.Game.Events.Context.Cards
{
    public readonly struct BeforeCardPositionChangeEvent
    {
        public IRuntimeCard RuntimeObject { get; }

        public BeforeCardPositionChangeEvent(IRuntimeCard runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}