namespace Client.Game.Abstractions.Runtime.Models
{
    public interface IRuntimeStatModel : IRuntimeModel
    {
        string DataId { get; }
        int RuntimeOwnerId { get; }
        int Max { get; set; }
        int Value { get; set; }
    }
}