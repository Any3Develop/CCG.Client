using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Shared.Abstractions.Common.Network;
using Shared.Common.Network.Data;

namespace Client.Common.Network
{
    public abstract class NetworkHubBase : INetworkHub
    {
        private readonly INetworkHubCaller networkHubCaller;
        private readonly INetworkSerializer networkSerializer;
        private readonly INetworkHubConnectionFactory hubConnectionFactory;
        private INetworkHubConnection hubConnection;
        
        protected abstract string HubUrl { get; }
        protected abstract int HubPort { get; }
        
        public bool IsConnected => hubConnection?.IsConnected ?? false;
        
        protected NetworkHubBase(
            INetworkHubCallerFactory networkHubCallerFactory,
            INetworkSerializer networkSerializer,
            INetworkHubConnectionFactory hubConnectionFactory)
        {
            this.networkSerializer = networkSerializer;
            this.hubConnectionFactory = hubConnectionFactory;
            networkHubCaller = networkHubCallerFactory.Create(GetType());
        }

        public async UniTask ConnectAsync(CancellationToken token = default)
        {
            if (IsConnected)
                hubConnection.Dispose();
            
            hubConnection = hubConnectionFactory.Create();
            hubConnection.OnMessageReceived += OnHubMessageReceived;
            await hubConnection.ConnectAsync(HubUrl, HubPort, token);
        }

        public async UniTask CloseAsync(CancellationToken token = default)
        {
            if (IsConnected)
                await hubConnection.CloseAsync(token);

            hubConnection = null;
        }

        public UniTask SendAsync(object target, object data, CancellationToken token = default)
        {
            return !IsConnected ? UniTask.CompletedTask : hubConnection.SendAsync(target, data, token);
        }

        private void OnHubMessageReceived(Message message)
        {
            var parameters = message.Args.Select(networkSerializer.Deserialize).ToArray();
            networkHubCaller.InvokeAsync(this, message.Target, CancellationToken.None, parameters).AsUniTask().Forget();
        }
    }
}