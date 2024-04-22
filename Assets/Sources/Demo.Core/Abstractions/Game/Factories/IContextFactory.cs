using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Collections;

namespace Demo.Core.Abstractions.Game.Factories
{
    public interface IContextFactory
    {
        IDatabase CreateDatabase(params object[] args);
        IRuntimePool CreateRuntimePool(params object[] args);
        IEffectsCollection CreateEffectsCollection(params object[] args);
        IStatsCollection CreateStatsCollection(params object[] args);
        IEventsSource CreateEventsSource(params object[] args);
    }
}