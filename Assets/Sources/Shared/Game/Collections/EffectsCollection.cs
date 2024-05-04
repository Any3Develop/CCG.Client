using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context.Logic;
using Shared.Abstractions.Game.Runtime.Effects;
using Shared.Game.Events.Effects;
using Shared.Game.Utils;

namespace Shared.Game.Collections
{
    public class EffectsCollection : RuntimeCollectionBase<IRuntimeEffect>, IEffectsCollection
    {
        private readonly IEventsSource eventsSource;

        public EffectsCollection(IEventsSource eventsSource)
        {
            this.eventsSource = eventsSource;
        }
        
        protected override int GetId(IRuntimeEffect value) =>
            value?.RuntimeData?.Id ?? int.MinValue;
        
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