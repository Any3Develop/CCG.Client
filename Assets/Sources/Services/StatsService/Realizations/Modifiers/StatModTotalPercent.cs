namespace CardGame.Services.StatsService.Modifiers
{
    public class StatModTotalPercent : StatModifier
    {
        public override int Order => 3;

        public override float ApplyModifier(Stat stat, float modValue)
        {
            return (stat.Value / 100) * modValue;
        }

        public StatModTotalPercent(float value, bool stacks = true, string modifierId = "") :
            base(value, stacks, modifierId)
        {
        }
    }
}