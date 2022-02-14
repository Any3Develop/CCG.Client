namespace CardGame.Services.StatsService
{
    public interface IStatModifier : IStatValueChanged
    {
        string Id { get; }
        int Order { get; }
        float Value { get; set; }
        bool Stacks { get; }
        float ApplyModifier(Stat stat, float modValue);
    }
}