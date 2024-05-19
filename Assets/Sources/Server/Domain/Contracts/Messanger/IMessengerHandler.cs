using Shared.Common.Network;

namespace Server.Domain.Contracts.Messanger
{
    public interface IMessengerHandler
    {
        void Handle(string userId, Message message);
    }
}