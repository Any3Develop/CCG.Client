using Server.Domain.Contracts.Sessions;
using Shared.Abstractions.Game.Context;

namespace Server.Application.Sessions
{
    public class SessionFactory : ISessionFactory
    {
        public ISession Create(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}