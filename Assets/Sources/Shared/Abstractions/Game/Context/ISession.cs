namespace Shared.Abstractions.Game.Context
{
    public interface ISession
    {
        string Id { get; }
        void Build(params object[] args);
    }
}