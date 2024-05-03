using Shared.Abstractions.Game.Runtime.Effects;

namespace Shared.Game.Events.Effects
{
    public readonly struct EffectAfterChangedEvent
    {
        public IRuntimeEffect Effect { get; }

        public EffectAfterChangedEvent(IRuntimeEffect effect)
        {
            Effect = effect;
        }
    }
}