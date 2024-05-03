namespace Shared.Abstractions.Common.Config
{
    public interface ISharedConfig
    {
        int MaxInTableCount { get; }
        int MaxInHandCount { get; }
        int MaxInDeckCount { get; }
    }
}