namespace Shared.Abstractions.Game.Context.Providers
{
    public interface IRuntimeNumberProvider
    {
        int Current { get; }
        int Next();
        void Sync(int value);
    }
}