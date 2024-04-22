namespace Demo.Core.Abstractions.Game.Context
{
    public interface IRuntimeIdGenerator
    {
        int Current { get; }
        int Next();
        void Sync(int value);
    }
}