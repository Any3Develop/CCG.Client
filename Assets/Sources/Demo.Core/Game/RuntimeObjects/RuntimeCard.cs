using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Game.Data;

namespace Demo.Core.Game.RuntimeObjects
{
    public class RuntimeCard : RuntimeObject
    {
        public new CardData Data => (CardData)base.Data;
        
        public RuntimeCard(
            IDatabase data, 
            IRuntimeObjectData runtimeData, 
            IRuntimeStatsCollection statsCollection, 
            IRuntimeEffectCollection effectCollection, 
            IEventsSource eventsSource) 
            : base(data, runtimeData, statsCollection, effectCollection, eventsSource) {}
        
    }
}