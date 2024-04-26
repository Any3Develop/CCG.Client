using Demo.Core.Abstractions.Game.Runtime.Cards;

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