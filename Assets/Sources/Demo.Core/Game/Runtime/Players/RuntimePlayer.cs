using System.Linq;
using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.Runtime.Data;
using Demo.Core.Abstractions.Game.Runtime.Objects;
using Demo.Core.Abstractions.Game.Runtime.Players;

namespace Demo.Core.Game.Runtime.Players
{
    public class RuntimePlayer : IRuntimePlayer
    {
        public IRuntimePlayerData RuntimeData { get; private set; }
        public IStatsCollection StatsCollection { get; private set; }
        public IEventsSource EventsSource { get; private set; }

        protected bool Initialized { get; private set; }

        public IRuntimePlayer Init(
            IRuntimePlayerData runtimeData,
            IStatsCollection statsCollection,
            IEventsSource eventsSource)
        {
            RuntimeData = runtimeData;
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