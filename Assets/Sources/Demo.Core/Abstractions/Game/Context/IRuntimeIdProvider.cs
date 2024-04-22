namespace Demo.Core.Abstractions.Game.Context
{
    public interface IRuntimeIdProvider
    {
        int Current { get; }
        int Next();
        void Sync(int value);
    }
}