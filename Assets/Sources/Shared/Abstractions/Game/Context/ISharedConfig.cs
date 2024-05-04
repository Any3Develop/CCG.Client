namespace Shared.Abstractions.Game.Context
{
    public interface ISharedConfig
    {
        int MaxInTableCount { get; }
        int MaxInHandCount { get; }
        int MaxInDeckCount { get; }
    }
}