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
using Shared.Common.Network.Data;
using Shared.Game.Utils;

namespace Server.Application.Messenger
{
    public class ServerTcpMessengerService : IMessengerService
    {
        private readonly IMessengerHandler messengerHandler;
        private readonly List<IClient> connected = new();
        private readonly object connectedLock = new();
        private CancellationTokenSource connection;
        private TcpListener listener;

        public IEnumerable<IClient> Clients
        {
            get
            {
                lock (connectedLock)
                    return connected.ToArray();
            }
        }
        
        public ServerTcpMessengerService(IMessengerHandler messengerHandler)
        {
            this.messengerHandler = messengerHandler;
        }
        
        public void Dispose()
        {
            Stop();
        }
        
        public void Start(int port)
        {
            try
            {
                connection?.Cancel();
                connection?.Dispose();
                connection = new CancellationTokenSource();
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                SharedLogger.Log($"[Server.{GetType().Name}] Started on port {port}");
                messengerHandler.CallBack += SendCallBack;
                AcceptClientsAsyncLoop(connection.Token).GetAwaiter();
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
                if (connection == null)
                    return;
                
                DropAllConnections();
                listener?.Stop();
                listener = null;
                connection?.Cancel();
                connection?.Dispose();
                connection = null;
                messengerHandler.CallBack -= SendCallBack;
                SharedLogger.Log($"[Server.{GetType().Name}] Shutdown.");
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
            
            var jsonData = JsonConvert.SerializeObject(message, SerializeExtensions.SerializeSettings);
            var messageBytes = Encoding.UTF8.GetBytes(jsonData);
            var lengthBytes = BitConverter.GetBytes(messageBytes.Length);
            
            await client.SendAsync(lengthBytes.Union(messageBytes).ToArray());
        }

        private void SendCallBack(IClient client, Message message)
        {
            SendAsync(client, message).GetAwaiter();
        }
        
        private async Task AcceptClientsAsyncLoop(CancellationToken token)
        {
            var acceptDelay = TimeSpan.FromSeconds(1);
            while (listener != null && !token.IsCancellationRequested)
            {
                await Task.Delay(acceptDelay, token);
                var connnection = await listener.AcceptTcpClientAsync();
                if (connnection is not {Connected: true} || TryGetClientByConnection(connnection, out _))
                    continue;
                
                SharedLogger.Log($"[Server.{GetType().Name}] Client connected.");
                
                var client = new TcpNetworkClient(connnection);
                SetConnected(client);
                HandleAsync(client, token).ConfigureAwait(false).GetAwaiter();
            }
        }
        
        private async Task HandleAsync(IClient client, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (!client.IsConnected)
                {
                    DropConnection(client);
                    return;
                }
                
                await ReceiveDelayTask(token);
                
                var lengthBuffer = new byte[4];
                var readLength = await client.ReadAsync(lengthBuffer, 0, lengthBuffer.Length, token);
                if (readLength == 0)
                    continue;
                
                var responseLength = BitConverter.ToInt32(lengthBuffer, 0);
                if (responseLength < 0)
                {
                    SharedLogger.Error($"[Server.{GetType().Name}] Client sent length-header less than zero");
                    continue;
                }
                
                var buffer = new byte[responseLength];
                if (await client.ReadAsync(buffer, readLength, buffer.Length, token) <= 0) 
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
                    
                    x.Abort();
                    return true;
                });
                
                client.Abort();
            }
        }

        private void DropAllConnections()
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
    }
}