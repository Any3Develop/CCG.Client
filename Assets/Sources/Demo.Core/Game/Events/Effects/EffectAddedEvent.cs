using Demo.Core.Abstractions.Game.Runtime.Effects;

namespace Demo.Core.Game.Events.Effects
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