using Shared.Abstractions.Game.Context;

namespace Server.Domain.Contracts.Sessions
{
    public interface ISessionFactory
    {
        ISession Create(string id);
    }
}