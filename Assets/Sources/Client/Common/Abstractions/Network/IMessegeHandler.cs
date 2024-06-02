using Shared.Common.Network;
using Shared.Common.Network.Data;

namespace Client.Common.Abstractions.Network
{
    public interface IMessegeHandler
    {
        void Handle(Message message);
    }
}