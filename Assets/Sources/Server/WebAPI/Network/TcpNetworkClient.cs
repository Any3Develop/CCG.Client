using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Server.Domain.Contracts;
using Shared.Common.Logger;
using Shared.Common.Network;
using Shared.Game.Utils;

namespace Server.WebAPI.Network
{
    public class TcpNetworkServer : IDisposable
    {
        private readonly IDbContext dbContext;
        private CancellationTokenSource connectionSource;
        private TcpListener listener;
        private List<TcpClient> unAuthorized = new();
        private readonly Dictionary<string, TcpClient> conencted = new();
        
        public TcpNetworkServer(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Start()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, Urls.Port);
                listener.Start();
                SharedLogger.Log($"Server started on port {Urls.Port}");
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
                listener?.Stop();
                listener = null;
                connectionSource?.Cancel();
                connectionSource = null;
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
            }
        }
        
        private async Task AcceptClientsAsyncLoop(CancellationToken token)
        {
            var acceptDelay = TimeSpan.FromSeconds(1);
            while (listener != null && !token.IsCancellationRequested)
            {
                var client = await listener.AcceptTcpClientAsync();
                if (client != null)
                    SharedLogger.Log($"Client connected : {client}");

                if (client.Connected)
                    SharedLogger.Log($"Client connected : {client}");
                else 
                    conencted.FirstOrDefault(x=> x.Value == client)
                HandleAsync(client, token).ConfigureAwait(false).GetAwaiter();
                await Task.Delay(acceptDelay, token).ConfigureAwait(false);
            }
        }

        public async Task SendAsync(string userId, Message message)
        {
            if (!conencted.TryGetValue(userId, out var client))
                return;
            
            await SendAsync(client, message);
        }

        public async Task SendAsync(TcpClient client, Message message)
        {
            if (client is not {Connected:true})
                return;
            
            await using var stream = client.GetStream();
            var jsonData = JsonConvert.SerializeObject(message, SerializeExtensions.SerializeSettings);
            var messageBytes = Encoding.UTF8.GetBytes(jsonData);
            var lengthPrefix = BitConverter.GetBytes(messageBytes.Length);
            
            await stream.WriteAsync(lengthPrefix);
            await stream.WriteAsync(messageBytes);
        }

        private async Task HandleAsync(TcpClient client, CancellationToken token)
        {
            await using var stream = client.GetStream();
            while (!token.IsCancellationRequested)
            {
                var lengthBuffer = new byte[4];
                if (await stream.ReadAsync(lengthBuffer, 0, lengthBuffer.Length, token) == 0)
                {
                    await ReceiveDelayTask(token);
                    continue;
                }
                
                var responseLength = BitConverter.ToInt32(lengthBuffer, 0);
                if (responseLength < 0)
                {
                    SharedLogger.Error("Client sent length header less than zero");
                    continue;
                }
                
                var buffer = new byte[responseLength];
                if (await stream.ReadAsync(buffer, 0, buffer.Length, token) > 0)
                {
                    try
                    {
                        var jsonData = Encoding.UTF8.GetString(buffer);
                        var message = JsonConvert.DeserializeObject<Message>(jsonData, SerializeExtensions.SerializeSettings);
                        if (message.Route == Route.Auth)
                        {
                            var user = dbContext.Users.FirstOrDefault(x => x.AccessToken == message.Data);
                            if (user == null)
                            {
                                message.Data = "Unauthorized: Access token un available.";
                                await SendAsync(client, message);
                                client.Close();
                                return;
                            }
                            
                            conencted[user.Id] = client;
                        }
                    }
                    catch (Exception e)
                    {
                        SharedLogger.Error(e);
                    }
                }
                
                await ReceiveDelayTask(token);
            }
        }

        private static Task ReceiveDelayTask(CancellationToken token)
        {
            return Task.Delay(1000, token);
        }

        public void Dispose()
        {
            connection?.Dispose();
            connection = null;
        }
    }
}