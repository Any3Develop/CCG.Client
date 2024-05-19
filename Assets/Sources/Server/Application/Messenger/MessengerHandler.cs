using System;
using Server.Domain.Contracts.Messanger;
using Server.Domain.Contracts.Persistence;
using Server.Domain.Contracts.Sessions;
using Shared.Common.Logger;
using Shared.Common.Network;

namespace Server.Application.Messenger
{
    public class MessengerHandler : IMessengerHandler
    {
        private readonly IDbContext dbContext;
        private readonly IRuntimeSessionRepository sessionRepository;
        private readonly ISessionFactory sessionFactory;

        public MessengerHandler(
            IDbContext dbContext,
            IRuntimeSessionRepository sessionRepository, 
            ISessionFactory sessionFactory)
        {
            this.dbContext = dbContext;
            this.sessionRepository = sessionRepository;
            this.sessionFactory = sessionFactory;
        }
        
        public void Handle(string userId, Message message)
        {
            switch (message.Route)
            {
                case Route.ClientInit:
                    break;
                case Route.GameEvent:
                    break;
                case Route.Command:
                    break;
                
                case Route.StartSession:
                    var session = sessionFactory.Create(Guid.NewGuid().ToString());
                    sessionRepository.Add(session);
                    session.Build();
                    break;
                
                case Route.Auth:
                default: SharedLogger.Warning($"Server cant handle unknown route : {message.Route}");
                    break;
            }
        }
    }
}