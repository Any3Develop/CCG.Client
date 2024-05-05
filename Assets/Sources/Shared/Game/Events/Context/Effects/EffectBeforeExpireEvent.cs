﻿using Shared.Abstractions.Game.Runtime.Effects;

namespace Shared.Game.Events.Context.Effects
{
    public readonly struct EffectBeforeExpireEvent
    {
        public IRuntimeEffect Effect { get; }

        public EffectBeforeExpireEvent(IRuntimeEffect effect)
        {
            Effect = effect;
        }
    }
}