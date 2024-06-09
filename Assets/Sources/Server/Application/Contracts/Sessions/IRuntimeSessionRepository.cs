using System.Collections.Generic;
using Shared.Abstractions.Game.Context;

namespace Server.Application.Contracts.Sessions
{
    public interface IRuntimeSessionRepository
    {
        void Add(ISession session);
        ISession Get(string id);
        ISession GetByUserId(string id);
        ISession GetFreeSession();
        IEnumerable<ISession> Get();
        bool Remove(string id);
    }
}