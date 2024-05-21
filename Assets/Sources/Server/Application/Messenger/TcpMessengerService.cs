using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Server.Domain.Contracts.Messanger;
using Shared.Common.Logger;
using Shared.Common.Network;
using Shared.Game.Utils;

namespace Server.Application.Messenger
{
    public class TcpMessengerService : IDisposable, IMessengerService
    {
        private readonly IMessengerHandler messengerHandler;
        private readonly List<IClient> connected = new();
        private readonly object connectedLock = new();
        private CancellationTokenSource connectionSource;
        private TcpListener listener;

        public IEnumerable<IClient> Clients
        {
            get
            {
                lock (connectedLock)
                    return connected.ToArray();
            }
        }
        
        public TcpMessengerService(IMessengerHandler messengerHandler)
        {
            this.messengerHandler = messengerHandler;
        }
        
        public void Start(int port)
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                SharedLogger.Log($"Server started on port {port}");
                messengerHandler.CallBack += (userId, msg) => SendAsync(userId, msg).GetAwaiter();
                AcceptClientsAsyncLoop(connectionSource.Token).GetAwaiter();
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
                Stop();
            }
        }

        public void Stop()
        {
            try
            {
                DropConnections();
                listener?.Stop();
                listener = null;
                connectionSource?.Cancel();
                connectionSource?.Dispose();
                connectionSource = null;
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
            }
        }

        public async Task BroadcastAsync(Message message)
        {
            Task[] clientsTasks;
            lock (connectedLock)
            {
                clientsTasks = connected.Select(x => SendAsync(x, message)).ToArray();
            }

            await Task.WhenAll(clientsTasks);
        }

        private async Task AcceptClientsAsyncLoop(CancellationToken token)
        {
            var acceptDelay = TimeSpan.FromSeconds(1);
            while (listener != null && !token.IsCancellationRequested)
            {
                await Task.Delay(acceptDelay, token).ConfigureAwait(false);
                var connnection = await listener.AcceptTcpClientAsync();
                if (connnection is not {Connected: true} || TryGetClientByConnection(connnection, out _))
                    continue;
                
                SharedLogger.Log($"Client connected : {connnection}");
                
                var client = new TcpNetworkClient(connnection);
                SetConnected(client);
                HandleAsync(client, token).ConfigureAwait(false).GetAwaiter();
            }
        }

        public async Task SendAsync(string userId, Message message)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                if (!TryGetClientById(userId, out var client))
                    return;
            
                await SendAsync(client, message);
                return;
            }

            await BroadcastAsync(message);
        }

        public async Task SendAsync(IClient client, Message message)
        {
            if (client is not {IsAuthorized:true, IsConnected: true})
                return;
            
            await using var stream = client.GetStream();
            var jsonData = JsonConvert.SerializeObject(message, SerializeExtensions.SerializeSettings);
            var messageBytes = Encoding.UTF8.GetBytes(jsonData);
            var lengthBytes = BitConverter.GetBytes(messageBytes.Length);
            
            await stream.WriteAsync(lengthBytes.Union(messageBytes).ToArray());
        }

        private async Task HandleAsync(IClient client, CancellationToken token)
        {
            await using var stream = client.GetStream();
            while (!token.IsCancellationRequested)
            {
                await ReceiveDelayTask(token);
                
                var lengthBuffer = new byte[4];
                var readLength = await stream.ReadAsync(lengthBuffer, 0, lengthBuffer.Length, token);
                if (readLength == 0)
                    continue;
                
                var responseLength = BitConverter.ToInt32(lengthBuffer, 0);
                if (responseLength < 0)
                {
                    SharedLogger.Error("Client sent length header less than zero");
                    continue;
                }
                
                var buffer = new byte[responseLength];
                if (await stream.ReadAsync(buffer, readLength, buffer.Length, token) <= 0) 
                    continue;
                
                try
                {
                    var jsonData = Encoding.UTF8.GetString(buffer);
                    var message = JsonConvert.DeserializeObject<Message>(jsonData, SerializeExtensions.GetDeserializeSettingsByType<Message>());
                    messengerHandler.Handle(client, message);
                }
                catch (Exception e)
                {
                    SharedLogger.Error(e);
                    await SendAsync(client, new Message
                    {
                        Route = Route.DropConnection,
                        Data = e.Message
                    });
                    DropConnection(client);
                    return;
                }
            }
        }

        private bool TryGetClientById(string userId, out IClient result)
        {
            lock (connectedLock)
            {
                result = connected.FirstOrDefault(x => x.IsAuthorized && x.UserId == userId);
                return result != null;
            }
        }

        private bool TryGetClientByConnection(TcpClient connection, out IClient result)
        {
            lock (connectedLock)
            {
                result = connected.FirstOrDefault(x => x.Equals(connection));
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
                connected.RemoveAll(x =>
                {
                    if (!x.Equals(client)) 
                        return false;
                    
                    x.Dispose();
                    return true;
                });
                
                client.Dispose();
            }
        }

        private void DropConnections()
        {
            lock (connectedLock)
            {
                foreach (var client in connected.ToArray())
                    DropConnection(client);
                
                connected.Clear();
            }
        }

        private static Task ReceiveDelayTask(CancellationToken token)
        {
            return Task.Delay(1000, token);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}