namespace Demo.Core.Abstractions.Game.RuntimeData
{
    public interface IRuntimeCardData : IRuntimeObjectData
    {
        int? Position { get; set; }
    }
}