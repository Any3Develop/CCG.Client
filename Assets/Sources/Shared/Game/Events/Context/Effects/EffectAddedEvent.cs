﻿using Shared.Abstractions.Game.Runtime.Effects;

namespace Shared.Game.Events.Context.Effects
{
    public readonly struct EffectAddedEvent
    {
        public IRuntimeEffect Effect { get; }

        public EffectAddedEvent(IRuntimeEffect effect)
        {
            Effect = effect;
        }
    }
}