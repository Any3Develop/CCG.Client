using Shared.Common.Network;

namespace Client.Common.Abstractions.Network
{
    public interface IMessegeHandler
    {
        void Handle(Message message);
    }
}