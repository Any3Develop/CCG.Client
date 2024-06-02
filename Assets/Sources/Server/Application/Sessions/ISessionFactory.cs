using Server.Domain.Contracts.Sessions;
using Shared.Abstractions.Game.Context;
using Shared.Abstractions.Game.Factories;

namespace Server.Application.Sessions
{
    public class SessionFactory : ISessionFactory
    {
        private readonly IContextFactory contextFactory;

        public SessionFactory(IContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        
        public ISession Create(string id)
        {
            return new RuntimeSession(id, contextFactory);
        }
    }
}