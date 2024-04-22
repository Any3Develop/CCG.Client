namespace Demo.Core.Abstractions.Game.RuntimeData
{
    public interface IRuntimeStatData : IRuntimeData
    {
        string Name { get; }
        int Base { get; set; }
        int Max { get; set; }
        int Value { get; set; }
    }
}