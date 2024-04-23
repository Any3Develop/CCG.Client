using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Game.Events.Effects
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