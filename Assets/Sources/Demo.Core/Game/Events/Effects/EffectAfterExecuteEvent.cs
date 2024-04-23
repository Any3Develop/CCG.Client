using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Game.Events.Effects
{
    public readonly struct EffectAfterExecuteEvent
    {
        public IRuntimeEffect Effect { get; }

        public EffectAfterExecuteEvent(IRuntimeEffect effect)
        {
            Effect = effect;
        }
    }
}