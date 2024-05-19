using Shared.Abstractions.Game.Context;

namespace Server.Domain.Contracts.Sessions
{
    public interface IRuntimeSessionRepository
    {
        void Add(ISession session);
        ISession Get(string id);
        bool Remove(string id);
    }
}