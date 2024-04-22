using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Game.RuntimeObjects
{
    public abstract class RuntimeEffect : IRuntimeEffect
    {
        public IDatabase Data { get; private set; }
        public IRuntimeEffectData RuntimeData { get; private set; }
        public IEventsSource EventsSource { get; private set; }

        protected RuntimeEffect(
            IDatabase data, 
            IRuntimeEffectData runtimeData, 
            IEventsSource eventsSource)
        {
            Data = data;
            RuntimeData = runtimeData;
            EventsSource = eventsSource;
        }

        public void Dispose()
        {
            EventsSource?.Dispose();
        }
    }
}