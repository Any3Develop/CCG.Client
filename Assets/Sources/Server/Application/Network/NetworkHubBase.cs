using System.Threading;
using System.Threading.Tasks;
using Server.Application.Contracts.Network;

namespace Server.Application.Network
{
    public abstract class NetworkHubBase : INetworkHub
    {
        private INetworkHubConnection hubConnection;
        protected IClient Caller {get; private set;}

        public INetworkHub SetupHub(INetworkHubConnection connection, IClient caller)
        {
            hubConnection = connection;
            Caller = caller;
            return this;
        }
        
        public Task BroadcastAsync(object target, object data = null, CancellationToken token = default)
        {
            return hubConnection.SendBroadcastAsync(target, data, token);
        }

        public Task SendAsync(string userId, object target, object data = null, CancellationToken token = default)
        {
            return hubConnection.SendAsync(userId, target, data, token);
        }

        public Task SendAsync(IClient client, object target, object data = null, CancellationToken token = default)
        {
            return hubConnection.SendAsync(client, target, data, token);
        }
    }
}