namespace Demo.Core.Abstractions.Game.Runtime.Data
{
    public interface IRuntimeStatData : IRuntimeDataBase
    {
        int RuntimeOwnerId { get; set; }
        int Max { get; set; }
        int Value { get; set; }
    }
}