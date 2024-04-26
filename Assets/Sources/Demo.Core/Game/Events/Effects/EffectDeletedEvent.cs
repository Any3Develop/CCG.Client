using Demo.Core.Abstractions.Game.Runtime.Effects;

namespace Demo.Core.Game.Events.Effects
{
    public readonly struct EffectDeletedEvent
    {
        public IRuntimeEffect Effect { get; }

        public EffectDeletedEvent(IRuntimeEffect effect)
        {
            Effect = effect;
        }
    }
}