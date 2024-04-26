using Demo.Core.Abstractions.Game.Runtime.Effects;

namespace Demo.Core.Game.Events.Effects
{
    public readonly struct EffectBeforeExecuteEvent
    {
        public IRuntimeEffect Effect { get; }

        public EffectBeforeExecuteEvent(IRuntimeEffect effect)
        {
            Effect = effect;
        }
    }
}