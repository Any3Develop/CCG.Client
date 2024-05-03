using Shared.Abstractions.Common.EventSource;
using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Runtime.Stats;
using Shared.Game.Events.Stats;
using Shared.Game.Utils;

namespace Shared.Game.Collections
{
    public class StatsCollection : RuntimeCollection<IRuntimeStat>, IStatsCollection
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