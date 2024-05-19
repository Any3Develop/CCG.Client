using System.Collections.Generic;
using System.Linq;
using Server.Domain.Contracts.Sessions;
using Shared.Abstractions.Game.Context;

namespace Server.Application.Sessions
{
    public class RuntimeSessionsRepository : IRuntimeSessionRepository
    {
        private readonly List<ISession> sessions = new();
        public void Add(ISession session)
        {
            if (sessions.Any(x=> x.Id == session.Id))
                return;
            
            sessions.Add(session);
        }

        public ISession Get(string id)
        {
            return sessions.Find(x => x.Id == id);
        }

        public bool Remove(string id)
        {
            return sessions.RemoveAll(x => x.Id == id) > 0;
        }
    }
}