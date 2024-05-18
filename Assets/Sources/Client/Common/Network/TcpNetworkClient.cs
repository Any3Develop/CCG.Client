using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Client.Common.Abstractions.Network;
using Client.Common.Network.Exceptions;
using Client.Game.Abstractions.Context.EventProcessors;
using Client.Game.Network;
using Client.Lobby.Runtime;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Shared.Common.Logger;
using Shared.Common.Network;
using Shared.Game.Utils;
using UnityEngine;

namespace Client.Common.Network
{
    public class TcpNetworkClient : IDisposable
    {
        private readonly IMessageHandler messageHandler;
        private readonly IGameEventQueueRemoteProcessor queueRemoteProcessor;
        private CancellationTokenSource connectionSource;
        private TcpClient connection;
        
        public TcpNetworkClient(IMessageHandler messageHandler)
        {
            this.messageHandler = messageHandler;
        }

        public async UniTask<bool> ConnectAsync()
        {
            try
            {
                if (connection is {Connected: true})
                    return true;
                
                Close();
                connection = new TcpClient();
                await connection.ConnectAsync(Urls.ServerUrl, Urls.Port);
                connectionSource = new CancellationTokenSource();
                await SendAsync(Route.Auth, User.AccessToken, connectionSource.Token);
                ReceiveAsync(connectionSource.Token).Forget();
                return true;
            }
            catch (Exception e)
            {
                Close();
                SharedLogger.Error(e);
                connectionSource?.Cancel();
                connectionSource = null;
                return false;
            }
        }

        public void Close()
        {
            try
            {
                connection?.Close();
                connection = null;
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
            }
        }

        public async UniTask SendAsync(Route route, object data, CancellationToken token = default)
        {
            if (connection is not {Connected:true})
                throw new NotConnectedException();
            
            await using var stream = connection.GetStream();
            var message = new Message
            {
                Route = route,
                Data = data as string ?? JsonConvert.SerializeObject(data, SerializeExtensions.SerializeSettings)
            };
            
            var jsonData = JsonConvert.SerializeObject(message, SerializeExtensions.SerializeSettings);
            var messageBytes = Encoding.UTF8.GetBytes(jsonData);
            var lengthPrefix = BitConverter.GetBytes(messageBytes.Length);
            
            await stream.WriteAsync(lengthPrefix, token);
            await stream.WriteAsync(messageBytes, token);
        }

        private async UniTask ReceiveAsync(CancellationToken token)
        {
            await using var stream = connection.GetStream();
            while (Application.isPlaying)
            {
                token.ThrowIfCancellationRequested();
                var lengthBuffer = new byte[4];
                if (await stream.ReadAsync(lengthBuffer, 0, lengthBuffer.Length, token) == 0)
                {
                    await ReceiveDelayTask(token);
                    continue;
                }
                
                var responseLength = BitConverter.ToInt32(lengthBuffer, 0);
                if (responseLength < 0)
                {
                    SharedLogger.Error("Server sent length header less than zero");
                    continue;
                }
                
                var buffer = new byte[responseLength];
                if (await stream.ReadAsync(buffer, 0, buffer.Length, token) > 0)
                {
                    try
                    {
                        var jsonData = Encoding.UTF8.GetString(buffer);
                        messageHandler.Handle(JsonConvert.DeserializeObject<Message>(jsonData, SerializeExtensions.SerializeSettings));
                    }
                    catch (Exception e)
                    {
                        SharedLogger.Error(e);
                    }
                }
                
                await ReceiveDelayTask(token);
            }
        }

        private static UniTask ReceiveDelayTask(CancellationToken token)
        {
            return UniTask.Delay(1000, DelayType.Realtime, PlayerLoopTiming.PostLateUpdate, token);
        }

        public void Dispose()
        {
            connection?.Dispose();
            connection = null;
        }
    }
}