namespace Demo.Core.Abstractions.Game.RuntimeData
{
    public interface IRuntimeStatData : IRuntimeDataBase
    {
        int RuntimeOwnerId { get; set; }
        int Max { get; set; }
        int Value { get; set; }
    }
}