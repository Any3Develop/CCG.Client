using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.RuntimeObjects;
using Demo.Core.Game.Events.Stats;
using Demo.Core.Game.Utils;

namespace Demo.Core.Game.Collections
{
    public class StatsCollection : RuntimeCollectionBase<IRuntimeStat>, IStatsCollection
    {
        private readonly IEventsSource eventsSource;
        public StatsCollection(IEventsSource eventsSource)
        {
            this.eventsSource = eventsSource;
        }

        public override bool Add(IRuntimeStat value, bool notify = true)
        {
            var result = base.Add(value, notify);
            eventsSource.Publish<StatAddedEvent>(notify && result, value);
            return result;
        }

        public override bool Remove(int id, bool notify = true)
        {
            if (!TryGet(id, out var value))
                return false;
            
            var result = base.Remove(value, notify);
            eventsSource.Publish<StatDeletedEvent>(notify && result, value);
            return result;
        }
    }
}