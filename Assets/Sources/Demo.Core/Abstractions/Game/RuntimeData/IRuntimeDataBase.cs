namespace Demo.Core.Abstractions.Game.RuntimeData
{
    public interface IRuntimeDataBase
    {
        int Id { get; }
        string DataId { get; }
        string OwnerId { get; }
    }
}