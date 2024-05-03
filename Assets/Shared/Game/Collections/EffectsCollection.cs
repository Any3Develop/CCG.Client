using Shared.Abstractions.Common.EventSource;
using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Runtime.Effects;
using Shared.Game.Events.Effects;
using Shared.Game.Utils;

namespace Shared.Game.Collections
{
    public class EffectsCollection : RuntimeCollection<IRuntimeEffect>, IEffectsCollection
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