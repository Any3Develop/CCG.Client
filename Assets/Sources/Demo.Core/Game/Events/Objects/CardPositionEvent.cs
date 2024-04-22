using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Game.Events.Objects
{
    public readonly struct CardPositionEvent
    {
        public IRuntimeCard RuntimeCard { get; }

        public CardPositionEvent(IRuntimeCard runtimeCard)
        {
            RuntimeCard = runtimeCard;
        }
    }
}