namespace Demo.Core.Abstractions.Game.RuntimeData
{
    public interface IRuntimeEffectData : IRuntimeData
    {
        int EffectOwnerId { get; set; }
        int Value { get; set; }
        int Lifetime { get; set; }
    }
}