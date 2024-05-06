using Shared.Abstractions.Game.Runtime.Cards;

namespace Shared.Game.Events.Context.Cards
{
    public readonly struct BeforeCardPositionChangedEvent
    {
        public IRuntimeCard RuntimeObject { get; }

        public BeforeCardPositionChangedEvent(IRuntimeCard runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}