namespace Demo.Core.Abstractions.Game.RuntimeData
{
    public interface IRuntimeEffectData : IRuntimeData
    {
        int Value { get; set; }
        int Lifetime { get; set; }
    }
}