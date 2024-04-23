using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Game.Events.Objects
{
    public readonly struct CardPositionChangedEvent
    {
        public IRuntimeCard RuntimeCard { get; }

        public CardPositionChangedEvent(IRuntimeCard runtimeCard)
        {
            RuntimeCard = runtimeCard;
        }
    }
}