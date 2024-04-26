using Demo.Core.Abstractions.Game.Runtime.Effects;

namespace Demo.Core.Game.Events.Effects
{
    public readonly struct EffectAfterExpireEvent
    {
        public IRuntimeEffect Effect { get; }

        public EffectAfterExpireEvent(IRuntimeEffect effect)
        {
            Effect = effect;
        }
    }
}