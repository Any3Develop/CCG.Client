namespace Client.Game.Abstractions.Runtime.Models
{
    public interface IRuntimeModel
    {
        int Id { get; }
        string OwnerId { get; }
    }
}