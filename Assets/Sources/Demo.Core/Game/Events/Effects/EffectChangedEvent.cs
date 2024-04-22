using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Game.Events.Effects
{
    public struct EffectChangedEvent
    {
        public IRuntimeEffect Effect { get; }

        public EffectChangedEvent(IRuntimeEffect effect)
        {
            Effect = effect;
        }
    }
}