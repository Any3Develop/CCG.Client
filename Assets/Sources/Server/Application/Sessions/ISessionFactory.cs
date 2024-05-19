using Server.Domain.Contracts.Sessions;
using Shared.Abstractions.Game.Context;
using Shared.Game.Data;

namespace Server.Application.Sessions
{
    public class SessionFactory : ISessionFactory
    {

        public ISession Create(string id, SessionPlayer[] players)
        {
            throw new System.NotImplementedException();
        }
    }
}