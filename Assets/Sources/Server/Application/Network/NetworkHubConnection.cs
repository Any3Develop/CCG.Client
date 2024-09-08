using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Server.Application.Contracts.Network;
using Shared.Abstractions.Common.Network;
using Shared.Common.Logger;
using Shared.Common.Network.Data;
using Shared.Common.Network.Routes;
using Shared.Game.Utils;

namespace Server.Application.Network
{
    public class NetworkHubConnection : INetworkHubConnection
    {
        private readonly INetworkSimulator networkSimulator;
        private readonly INetworkSerializer networkSerializer;
        private readonly INetworkMessageFactory networkMessageFactory;
        private readonly INetworkStreamFactory networkStreamFactory;
        private readonly List<IClient> connected = new();
        private readonly object connectedLock = new();
        private TcpListener listener;

        public bool IsConnected => listener?.Server is {Connected: true};
        public event Action<IClient, Message> OnMessageReceived;

        public IEnumerable<IClient> Clients
        {
            get
            {
                lock (connectedLock)
                    return connected.ToArray();
            }
        }

        public NetworkHubConnection(
            INetworkSimulator networkSimulator,
            INetworkSerializer networkSerializer,
            INetworkMessageFactory networkMessageFactory,
            INetworkStreamFactory networkStreamFactory)
        {
            this.networkSimulator = networkSimulator;
            this.networkSerializer = networkSerializer;
            this.networkMessageFactory = networkMessageFactory;
            this.networkStreamFactory = networkStreamFactory;
        }

        public void Dispose()
        {
            CloseAsync()
                .ConfigureAwait(false)
                .GetAwaiter();
            OnMessageReceived = null;
        }

        public async Task ConnectAsync(int hubPort, CancellationToken token = default)
        {
            try
            {
                await CloseAsync(token);
                listener = new TcpListener(IPAddress.Any, hubPort);
                listener.Start();
                SharedLogger.Log($"[Server.{GetType().Name}] Started on port {hubPort}");

                AcceptClientsAsyncLoop(token).GetAwaiter();
            }
            catch (Exception e) when (e is OperationCanceledException or ObjectDisposedException)
            {
                // Nothing to do
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
                await CloseAsync(token);
            }
        }

        public Task CloseAsync(CancellationToken token = default)
        {
            try
            {
                if (listener == null)
                    return Task.CompletedTask;

                DropAllConnections();
                listener?.Stop();
                listener = null;
                SharedLogger.Log($"[Server.{GetType().Name}] Shutdown.");
                return Task.CompletedTask;
            }
            catch (Exception e) when (e is OperationCanceledException or ObjectDisposedException)
            {
                // Nothing to do
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
                throw;
            }
        }

        public Task SendBroadcastAsync(object target, object data = null, CancellationToken token = default)
        {
            try
            {
                return Task.WhenAll(Clients.Select(x => SendAsync(x, target, data, token)));
            }
            catch (Exception e) when (e is OperationCanceledException or ObjectDisposedException)
            {
                // Nothing to do
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
                throw;
            }
        }

        public Task SendAsync(string userId, object target, object data = null, CancellationToken token = default)
        {
            try
            {
                return !TryGetClientById(userId, out var client)
                    ? SendAsync(client, target, data, token)
                    : Task.CompletedTask;
            }
            catch (Exception e) when (e is OperationCanceledException or ObjectDisposedException)
            {
                // Nothing to do
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
                throw;
            }
        }

        public Task SendAsync(IClient client, object target, object data = null, CancellationToken token = default)
        {
            try
            {
                if (client is not {IsConnected: true})
                    return Task.CompletedTask;

                var message = networkMessageFactory.Create(target, data);
                var messageBytes = networkSerializer.Serialize(message);

                SharedLogger.Log($"[Server.{GetType().Name}] Send to client a message : {message.ReflectionFormat()}");
                return client.NetworkStream.WriteAsync(messageBytes, token);
            }
            catch (Exception e) when (e is OperationCanceledException or ObjectDisposedException)
            {
                // Nothing to do
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
                throw;
            }
        }

        private async Task AcceptClientsAsyncLoop(CancellationToken token)
        {
            try
            {
                while (listener != null && !token.IsCancellationRequested)
                {
                    await networkSimulator.TickOptimizerAsync(token);
                    var connnection = await listener.AcceptTcpClientAsync();
                    if (connnection is not {Connected: true})
                        continue;

                    SharedLogger.Log($"[Server.{GetType().Name}] Client connected.");

                    var client = new NetworkClient(connnection, networkStreamFactory.Create(connnection.GetStream()));
                    SetConnected(client);
                    ReceiveAsync(client, token)
                        .ConfigureAwait(false)
                        .GetAwaiter();
                }
            }
            catch (Exception e) when (e is OperationCanceledException or ObjectDisposedException)
            {
                // Nothing to do
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
            }
        }

        private async Task ReceiveAsync(IClient client, CancellationToken token)
        {
            try
            {
                while (client is {IsConnected: true})
                {
                    await networkSimulator.TickOptimizerAsync(token);
                    var result = await client.NetworkStream.ReadAsync(token);
                    if (!result.Successful)
                        continue;

                    var message = networkSerializer.Deserialize<Message>(result.Buffer);
                    OnMessageReceived?.Invoke(client, message);
                }
            }
            catch (Exception e) when (e is OperationCanceledException or ObjectDisposedException)
            {
                // Nothing to do
            }
            catch (Exception e)
            {
                await SendDropConnection(e);
            }
            finally
            {
                DropConnection(client);
            }

            return;

            Task SendDropConnection(Exception e)
            {
                try
                {
                    SharedLogger.Error(e);
                    // try to send disconnection message
                    return SendAsync(client, GlobalAction.CloseConnection, e.Message, token);
                }
                catch (Exception other)
                {
                    SharedLogger.Error($"{nameof(SendDropConnection)} throw an Exception : {other}.\n" +
                                       $"While send drop with with reason : {e}");
                    // Nothing to do.
                    return Task.CompletedTask;
                }
            }
        }

        private bool TryGetClientById(string userId, out IClient result)
        {
            lock (connectedLock)
            {
                result = connected.FirstOrDefault(x => x.ClientId == userId);
                return result != null;
            }
        }

        private void SetConnected(IClient client)
        {
            lock (connectedLock)
            {
                if (connected.Contains(client))
                    return;

                connected.Add(client);
            }
        }

        private void DropConnection(IClient client)
        {
            if (client == null)
                return;

            lock (connectedLock)
            {
                if (connected.Count > 0)
                    connected.RemoveAll(x =>
                    {
                        if (!x.Equals(client))
                            return false;

                        x.Abort();
                        return true;
                    });
            }

            client.Abort();
            SharedLogger.Log($"[Server.{GetType().Name}] Client disconnected.");
        }

        private void DropAllConnections()
        {
            foreach (var client in Clients)
                DropConnection(client);
        }
    }
}