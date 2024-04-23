using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Abstractions.Game.RuntimeObjects;
using Demo.Core.Game.Data;
using Demo.Core.Game.Events.Objects;
using Demo.Core.Game.Utils;

namespace Demo.Core.Game.RuntimeObjects
{
    public class RuntimeCard : RuntimeObject, IRuntimeCard
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