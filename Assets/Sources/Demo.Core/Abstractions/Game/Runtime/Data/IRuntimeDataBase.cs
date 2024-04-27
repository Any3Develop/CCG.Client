namespace Demo.Core.Abstractions.Game.Runtime.Data
{
    public interface IRuntimeDataBase
    {
        int Id { get; }
        string OwnerId { get; }
    }
}