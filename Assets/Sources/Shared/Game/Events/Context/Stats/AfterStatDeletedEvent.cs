using Shared.Abstractions.Game.Runtime.Stats;

namespace Shared.Game.Events.Context.Stats
{
    public readonly struct AfterStatDeletedEvent
    {
        public IRuntimeStat RuntimeStat { get; }

        public AfterStatDeletedEvent(IRuntimeStat runtimeStat)
        {
            RuntimeStat = runtimeStat;
        }
    }
}