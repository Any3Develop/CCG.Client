using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.RuntimeObjects;
using Demo.Core.Game.Events.Stats;

namespace Demo.Core.Game.Collections
{
    public class StatsCollection : RuntimeCollectionBase<IRuntimeStat>, IStatsCollection
    {
        private readonly IEventsSource eventsSource;
        public StatsCollection(IEventsSource eventsSource)
        {
            this.eventsSource = eventsSource;
        }

        public override bool Add(IRuntimeStat value)
        {
            var added = base.Add(value);
            eventsSource.Publish(new StatAddedEvent(value));
            return added;
        }

        public override bool Remove(int id)
        {
            if (!TryGet(id, out var stat))
                return false;
            
            var removed = base.Remove(stat);
            eventsSource.Publish(new StatDeletedEvent(stat));
            return removed;
        }
    }
}