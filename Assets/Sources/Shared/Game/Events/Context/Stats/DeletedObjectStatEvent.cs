using Shared.Abstractions.Game.Runtime.Stats;

namespace Shared.Game.Events.Context.Stats
{
    public readonly struct DeletedObjectStatEvent
    {
        public IRuntimeStat RuntimeStat { get; }

        public DeletedObjectStatEvent(IRuntimeStat runtimeStat)
        {
            RuntimeStat = runtimeStat;
        }
    }
}