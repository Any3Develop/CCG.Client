using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Game.Collections
{
    public class EffectsCollection : RuntimeCollectionBase<IRuntimeEffect>, IEffectsCollection
    {
        private readonly IEventsSource eventsSource;
        public EffectsCollection(IEventsSource eventsSource)
        {
            this.eventsSource = eventsSource;
        }
    }
}