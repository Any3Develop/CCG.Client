using Demo.Core.Abstractions.Game.Runtime.Cards;
using Demo.Core.Abstractions.Game.Runtime.Data;
using Demo.Core.Game.Data;
using Demo.Core.Game.Events.Objects;
using Demo.Core.Game.Runtime.Objects;
using Demo.Core.Game.Utils;

namespace Demo.Core.Game.Runtime.Cards
{
    public abstract class RuntimeCardBase : RuntimeObject, IRuntimeCard
    {
        public new CardData Data => (CardData) base.Data;
        public new IRuntimeCardData RuntimeData => (IRuntimeCardData) base.RuntimeData;

        public void SetPosition(int? value, bool notify = true)
        {
            if (!Initialized)
                return;
            
            RuntimeData.Position = value;
            EventsSource.Publish<CardPositionChangedEvent>(notify, this);
        }
    }
}