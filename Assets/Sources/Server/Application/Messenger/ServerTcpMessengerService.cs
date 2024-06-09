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
                Stop();
                connection = new CancellationTokenSource();
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                SharedLogger.Log($"[Server.{GetType().Name}] Started on port {port}");
                
                messengerHandler.CallBack += SendCallBack;
                AcceptClientsAsyncLoop(connection.Token).GetAwaiter();
            }
            catch (OperationCanceledException)
            {
                // Nothing to do
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
            catch (OperationCanceledException)
            {
                // Nothing to do
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
            }
        }

        public async Task BroadcastAsync(Message message, CancellationToken token = default)
        {
            try
            {
                await Task.WhenAll(Clients.Select(x => SendAsync(x, message, token)));
            }
            catch (OperationCanceledException)
            {
                // Nothing to do
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
                throw;
            }
        }
        
        public async Task SendAsync(string userId, Message message, CancellationToken token = default)
        {
            try
            {
                if (!TryGetClientById(userId, out var client))
                    await SendAsync(client, message, token);
            }
            catch (OperationCanceledException)
            {
                // Nothing to do
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
                throw;
            }
        }

        public async Task SendAsync(IClient client, Message message, CancellationToken token = default)
        {
            try
            {
                if (client is not {IsConnected: true})
                    return;
                
                var jsonData = JsonConvert.SerializeObject(message, SerializeExtensions.SerializeSettings);
                var messageBytes = Encoding.UTF8.GetBytes(jsonData);
                var lengthBytes = BitConverter.GetBytes(messageBytes.Length);

                var stream = client.GetStream();
                while (stream is {CanWrite: false})
                    await NetworkDelayTask(500, token);
            
                if (stream is not {CanWrite: true})
                {
                    SharedLogger.Error($"[Server.{GetType().Name}] Can't send a message because connection with Client is closed.");
                    return;
                }
                
                await stream.WriteAsync(lengthBytes.Concat(messageBytes).ToArray(), token);
                SharedLogger.Log($"[Server.{GetType().Name}] Sent to client a message : {message.ReflectionFormat()}");
            }
            catch (OperationCanceledException)
            {
                // Nothing to do
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
                throw;
            }
        }

        private void SendCallBack(IClient client, Message message)
        {
            SendAsync(client, message).GetAwaiter();
        }
        
        private async Task AcceptClientsAsyncLoop(CancellationToken token)
        {
            try
            {
                while (listener != null && !token.IsCancellationRequested)
                {
                    await NetworkDelayTask(token: token);
                    var connnection = await listener.AcceptTcpClientAsync();
                    if (connnection is not {Connected: true})
                        continue;

                    SharedLogger.Log($"[Server.{GetType().Name}] Client connected. \n{connection.ReflectionFormat()}");

                    var client = new TcpNetworkClient(connnection);
                    SetConnected(client);
                    ReceiveAsync(client, token).GetAwaiter();
                }
            }
            catch (OperationCanceledException)
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
                var stream = client.GetStream();
                var lengthBuffer = new byte[4];
                while (client is {IsConnected: true})
                {
                    await NetworkDelayTask(token: token);
                    if (!stream.CanRead)
                        continue;
                    
                    var readLength = await stream.ReadAsync(lengthBuffer, 0, lengthBuffer.Length, token);
                    if (readLength == 0)
                        continue;

                    var responseLength = BitConverter.ToInt32(lengthBuffer, 0);
                    if (responseLength < 0)
                    {
                        SharedLogger.Error($"[Server.{GetType().Name}] Client sent incorrect header.");
                        continue;
                    }

                    var buffer = new byte[responseLength];
                    if (await stream.ReadAsync(buffer, 0, buffer.Length, token) <= 0)
                        continue;

                    var data = Encoding.UTF8.GetString(buffer);
                    var message = JsonConvert.DeserializeObject<Message>(data, SerializeExtensions.SerializeSettings);
                    messengerHandler.Handle(client, message);
                }
            }
            catch (OperationCanceledException)
            {
                // Nothing to do
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
                try
                {
                    // try to send disconnection message
                    await SendAsync(client, new Message
                    {
                        Route = Route.DropConnection,
                        Data = e.Message
                    }, token);
                }
                catch
                {
                    // Nothing to do.
                }
            }
            finally
            {
                DropConnection(client);
            }
        }

        private bool TryGetClientById(string userId, out IClient result)
        {
            lock (connectedLock)
            {
                result = connected.FirstOrDefault(x => x.UserId == userId);
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

        private static Task NetworkDelayTask(int miliseconds = 1000, CancellationToken token = default)
        {
            return Task.Delay(miliseconds, token);
        }
    }
}