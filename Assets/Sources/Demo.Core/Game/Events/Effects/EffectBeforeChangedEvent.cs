using Demo.Core.Abstractions.Game.Runtime.Effects;

namespace Demo.Core.Game.Events.Effects
{
    public readonly struct EffectBeforeChangedEvent
    {
        public IRuntimeEffect Effect { get; }

        public EffectBeforeChangedEvent(IRuntimeEffect effect)
        {
            Effect = effect;
        }
    }
}