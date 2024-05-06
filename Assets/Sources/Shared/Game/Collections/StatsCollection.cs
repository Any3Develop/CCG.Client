using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context.EventSource;
using Shared.Abstractions.Game.Runtime.Stats;
using Shared.Game.Events.Context.Stats;
using Shared.Game.Utils;

namespace Shared.Game.Collections
{
    public class StatsCollection : RuntimeCollectionBase<IRuntimeStat>, IStatsCollection
    {
        private readonly IEventsSource eventsSource;
        public StatsCollection(IEventsSource eventsSource)
        {
            this.eventsSource = eventsSource;
        }

        protected override int GetId(IRuntimeStat value) =>
            value?.RuntimeData?.Id ?? int.MinValue;
        
        public override bool Add(IRuntimeStat value, bool notify = true)
        {
            var result = base.Add(value, notify);
            eventsSource.Publish<AddedObjectStatEvent>(notify && result, value);
            return result;
        }

        public override bool Remove(int id, bool notify = true)
        {
            if (!TryGet(id, out var value))
                return false;
            
            var result = base.Remove(value, notify);
            eventsSource.Publish<DeletedObjectStatEvent>(notify && result, value);
            return result;
        }
    }
}