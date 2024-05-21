using Shared.Game.Data;

namespace Shared.Abstractions.Game.Context
{
    public interface ISession
    {
        SessionPlayer[] Players { get; }
        IContext Context { get; }
        string Id { get; }
        void Build(params object[] args);
        void Start();
        void AddPlayer(SessionPlayer player);
    }
}