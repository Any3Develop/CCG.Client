using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.RuntimeObjects;
using Demo.Core.Game.Events.Effects;
using Demo.Core.Game.Utils;

namespace Demo.Core.Game.Collections
{
    public class EffectsCollection : RuntimeCollectionBase<IRuntimeEffect>, IEffectsCollection
    {
        private readonly IEventsSource eventsSource;

        public EffectsCollection(IEventsSource eventsSource)
        {
            this.eventsSource = eventsSource;
        }

        public override bool Add(IRuntimeEffect value, bool notify = true)
        {
            var result = base.Add(value, notify);
            eventsSource.Publish<EffectAddedEvent>(notify && result, value);
            return result;
        }

        public override bool Remove(int id, bool notify = true)
        {
            if (!TryGet(id, out var value))
                return false;

            var result = base.Remove(value, notify);
            eventsSource.Publish<EffectDeletedEvent>(notify && result, value);
            return result;
        }
    }
}