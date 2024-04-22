using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.RuntimeObjects;
using Demo.Core.Game.Events.Effects;

namespace Demo.Core.Game.Collections
{
    public class EffectsCollection : RuntimeCollectionBase<IRuntimeEffect>, IEffectsCollection
    {
        private readonly IEventsSource eventsSource;
        public EffectsCollection(IEventsSource eventsSource)
        {
            this.eventsSource = eventsSource;
        }
        
        public override bool Add(IRuntimeEffect value)
        {
            var added = base.Add(value);
            eventsSource.Publish(new EffectAddedEvent(value));
            return added;
        }

        public override bool Remove(int id)
        {
            if (!TryGet(id, out var stat))
                return false;
            
            var removed = base.Remove(stat);
            eventsSource.Publish(new EffectDeletedEvent(stat));
            return removed;
        }
    }
}