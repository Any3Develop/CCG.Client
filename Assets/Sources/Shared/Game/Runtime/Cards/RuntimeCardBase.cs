using Shared.Abstractions.Game.Runtime.Cards;
using Shared.Abstractions.Game.Runtime.Data;
using Shared.Game.Data;
using Shared.Game.Events.Context.Objects;
using Shared.Game.Runtime.Objects;
using Shared.Game.Utils;

namespace Shared.Game.Runtime.Cards
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