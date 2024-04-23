using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Game.Events.Effects
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