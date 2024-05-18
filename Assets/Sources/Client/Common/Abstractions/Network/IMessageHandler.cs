using Shared.Common.Network;

namespace Client.Common.Abstractions.Network
{
    public interface IMessageHandler
    {
        void Handle(Message message);
    }
}