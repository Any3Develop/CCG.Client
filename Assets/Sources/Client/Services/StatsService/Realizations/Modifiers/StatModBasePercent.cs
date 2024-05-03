namespace CardGame.Services.StatsService.Modifiers
{
    public class StatModBasePercent : StatModifier
    {
        public override int Order => 1;

        public override float ApplyModifier(Stat stat, float modValue)
        {
            return (stat.BaseValue / 100) * modValue;
        }

        public StatModBasePercent(float value, bool stacks = true, string modifierId = "") :
            base(value, stacks, modifierId)
        {
        }
    }
}