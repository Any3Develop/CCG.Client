using System;
using System.Net.Sockets;
using System.Threading;
using Cysharp.Threading.Tasks;
using Shared.Abstractions.Common.Network;
using Shared.Common.Logger;
using Shared.Common.Network.Data;
using Shared.Common.Network.Exceptions;

namespace Client.Common.Network
{
    public class NetworkHubConnection : INetworkHubConnection
    {
        private readonly INetworkSimulator networkSimulator;
        private readonly INetworkStreamFactory networkStreamFactory;
        private readonly INetworkMessageFactory networkMessageFactory;
        private readonly INetworkSerializer networkSerializer;
        
        private INetworkStream networkStream;
        private CancellationTokenSource connectionSource;
        private TcpClient client;
        public bool IsConnected => client is {Connected: true};
        public event Action<Message> OnMessageReceived;
        
        public NetworkHubConnection(
            INetworkSimulator networkSimulator,
            INetworkStreamFactory networkStreamFactory,
            INetworkMessageFactory networkMessageFactory, 
            INetworkSerializer networkSerializer)
        {
            this.networkSimulator = networkSimulator;
            this.networkStreamFactory = networkStreamFactory;
            this.networkMessageFactory = networkMessageFactory;
            this.networkSerializer = networkSerializer;
        }

        public void Dispose()
        {
            CloseAsync().Forget();
            OnMessageReceived = null;
        }

        public async UniTask ConnectAsync(string hubAdderss, int hubPort, CancellationToken token = default)
        {
            try
            {
                await CloseAsync(token);
                client = new TcpClient();
                await client.ConnectAsync(hubAdderss, hubPort);
                networkStream = networkStreamFactory.Create(client.GetStream());
                connectionSource = CancellationTokenSource.CreateLinkedTokenSource(token);
                SharedLogger.Log($"[Client.{GetType().Name}] Connected to the server!");
                ReceiveAsync(connectionSource.Token).Forget();
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
                await CloseAsync(token);
                throw;
            }
        }

        public UniTask CloseAsync(CancellationToken token = default)
        {
            try
            {
                if (client == null)
                    return UniTask.CompletedTask;

                client?.Close();
                client?.Dispose();
                networkStream = null;
                client = null;

                connectionSource?.Cancel();
                connectionSource?.Dispose();
                connectionSource = null;
                SharedLogger.Log($"[Client.{GetType().Name}] Shutdown.");
            }
            catch (Exception e) when (e is OperationCanceledException or ObjectDisposedException)
            {
                // Nothing to do.
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
            }
            return UniTask.CompletedTask;
        }

        public async UniTask SendAsync(object target, object data, CancellationToken token = default)
        {
            if (client is not {Connected:true} || networkStream == null)
            {
                await CloseAsync(token);
                throw new NotConnectedException();
            }

            var message = networkMessageFactory.Create(target, data);
            var messageBytes = networkSerializer.Serialize(message);
            
            await networkSimulator.WaitRandomAsync(token).AsUniTask();
            await networkStream.WriteAsync(messageBytes, token);
        }
        
        private async UniTask ReceiveAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested && client is {Connected: true})
                {
                    // receive optimization because the game doesnt required every tick receive data.
                    await networkSimulator.TickOptimizerAsync(token).AsUniTask();
                    
                    var result = await networkStream.ReadAsync(token);
                    if (!result.Successful)
                        continue;

                    var message = networkSerializer.Deserialize<Message>(result.Buffer);
                    OnMessageReceived?.Invoke(message);
                }
            }
            catch (Exception e) when (e is OperationCanceledException or ObjectDisposedException)
            {
                // Nothing to do.
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
            }
            finally
            {
                await CloseAsync(token);
            }
        }
    }
}