using Shared.Abstractions.Game.Runtime.Stats;

namespace Shared.Game.Events.Context.Stats
{
    public readonly struct BeforeStatChangedEvent
    {
        public IRuntimeStat RuntimeStat { get; }

        public BeforeStatChangedEvent(IRuntimeStat runtimeStat)
        {
            RuntimeStat = runtimeStat;
        }
    }
}