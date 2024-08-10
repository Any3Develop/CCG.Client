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
        public IEventPublisher EventPublisher { get; private set; }
        public IEventsSource EventsSource { get; private set; }

        protected bool Initialized { get; private set; }

        public RuntimePlayer(
            IStatsCollection statsCollection, 
            IEventPublisher eventPublisher,
            IEventsSource eventsSource)
        {
            EventPublisher = eventPublisher;
            StatsCollection = statsCollection;
            EventsSource = eventsSource;
        }

        public void Dispose()
        {
            if (!Initialized)
                return;

            Initialized = false;
            EventsSource?.Dispose();
            StatsCollection?.Dispose();
        }

        public IRuntimePlayer Sync(IRuntimePlayerData runtimeData, bool notify = true)
        {
            Initialized = true;
            EventPublisher.Publish<BeforePlayerChangeEvent>(notify, this);
            RuntimeData = runtimeData;
            EventPublisher.Publish<AfterPlayerChangedEvent>(notify, this);
            return this;
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

        public void SetReady(bool value)
        {
            RuntimeData.Ready = value;
        }

        #region IRuntimeObjectBase

        IRuntimeDataBase IRuntimeObjectBase.RuntimeData => RuntimeData;

        #endregion
    }
}