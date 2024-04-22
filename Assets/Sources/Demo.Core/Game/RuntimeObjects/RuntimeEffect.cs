using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Abstractions.Game.RuntimeObjects;
using Demo.Core.Game.Data;

namespace Demo.Core.Game.RuntimeObjects
{
    public abstract class RuntimeEffect : IRuntimeEffect
    {
        public EffectData Data { get; private set; }
        public IRuntimeEffectData RuntimeData { get; private set; }
        public IEventsSource EventsSource { get; private set; }

        public void Init(
            EffectData data, 
            IRuntimeEffectData runtimeData, 
            IEventsSource eventsSource)
        {
            Data = data;
            RuntimeData = runtimeData;
            EventsSource = eventsSource;
        }
        
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
        
        #region IRuntimeBase

        IRuntimeData IRuntimeBase.RuntimeData => RuntimeData;
        IData IRuntimeBase.Data => Data;

        #endregion
    }
}