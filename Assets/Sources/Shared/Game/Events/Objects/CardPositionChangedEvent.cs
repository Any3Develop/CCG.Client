using Shared.Abstractions.Game.Runtime.Cards;

namespace Shared.Game.Events.Objects
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