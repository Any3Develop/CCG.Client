﻿using Shared.Abstractions.Game.Runtime.Stats;

namespace Shared.Game.Events.Stats
{
    public readonly struct AfterStatChangedEvent
    {
        public IRuntimeStat RuntimeStat { get; }

        public AfterStatChangedEvent(IRuntimeStat runtimeStat)
        {
            RuntimeStat = runtimeStat;
        }
    }
}