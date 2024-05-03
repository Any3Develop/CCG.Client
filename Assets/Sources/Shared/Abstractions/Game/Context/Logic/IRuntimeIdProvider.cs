namespace Shared.Abstractions.Game.Context.Logic
{
    public interface IRuntimeIdProvider
    {
        int Current { get; }
        int Next();
        void Sync(int value);
    }
}