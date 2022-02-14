namespace CardGame.Services.StatsService.Modifiers
{ 
    public class StatModBaseAdd : StatModifier
    {
        public override int Order => 2;
        public override float ApplyModifier(Stat stat, float modValue)
        {
            return modValue;
        }

        public StatModBaseAdd( float value, bool stacks = true, string modifierId = "") : base(value, stacks, modifierId) 
        { }
    }
}