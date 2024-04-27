namespace Demo.Core.Abstractions.Game.Runtime.Data
{
    public interface IRuntimeEffectData : IRuntimeDataBase
    {
        string DataId { get; }
        int EffectOwnerId { get; set; }
        int Value { get; set; }
        int Lifetime { get; set; }
    }
}