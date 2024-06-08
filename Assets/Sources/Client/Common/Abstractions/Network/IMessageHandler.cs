using Shared.Common.Network.Data;

namespace Client.Common.Abstractions.Network
{
    public interface IMessageHandler
    {
        void Handle(Message message);
    }
}