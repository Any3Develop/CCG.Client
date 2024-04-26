namespace Demo.Core.Abstractions.Game.Runtime.Data
{
    public interface IRuntimeCardData : IRuntimeObjectData
    {
        int? Position { get; set; }
    }
}