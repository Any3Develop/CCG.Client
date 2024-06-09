using Shared.Abstractions.Game.Context;

namespace Server.Application.Contracts.Sessions
{
    public interface ISessionFactory
    {
        ISession Create(string id);
    }
}