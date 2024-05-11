using System.Linq;
using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context.EventSource;
using Shared.Abstractions.Game.Runtime.Data;
using Shared.Abstractions.Game.Runtime.Objects;
using Shared.Abstractions.Game.Runtime.Players;
using Shared.Game.Events.Context.Players;
using Shared.Game.Utils;

namespace Shared.Game.Runtime.Players
{
    public class RuntimePlayer : IRuntimePlayer
    {
        public IRuntimePlayerData RuntimeData { get; private set; }
        public IStatsCollection StatsCollection { get; private set; }
        public IEventsSource EventsSource { get; private set; }

        protected bool Initialized { get; private set; }

        public IRuntimePlayer Init(
            IStatsCollection statsCollection,
            IEventsSource eventsSource)
        {
            StatsCollection = statsCollection;
            EventsSource = eventsSource;
            Initialized = true;
            return this;
        }

        public void Dispose()
        {
            if (!Initialized)
                return;

            Initialized = false;
            EventsSource?.Dispose();
            StatsCollection?.Dispose();
        }

        public void Sync(IRuntimePlayerData runtimeData, bool notify = true)
        {
            if (!Initialized)
                return;
            
            EventsSource.Publish<BeforePlayerChangeEvent>(notify, this);
            RuntimeData = runtimeData;
            EventsSource.Publish<AfterPlayerChangedEvent>(notify, this);
        }

        public bool TrySpendMana(int value)
        {
            if (!Initialized || value < 0)
                return false;

            var runtimeStat = StatsCollection.First();
            if (runtimeStat == null)
                return false;

            var remainder = runtimeStat.RuntimeData.Value - value;
            if (remainder < 0)
                return false;
            
            runtimeStat.SetValue(remainder);
            return true;
        }

        #region IRuntimeObjectBase

        IRuntimeDataBase IRuntimeObjectBase.RuntimeData => RuntimeData;

        #endregion
    }
}