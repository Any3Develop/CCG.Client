using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Client.Common.Abstractions.Network;
using Client.Common.Network.Exceptions;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Shared.Common.Logger;
using Shared.Common.Network.Data;
using Shared.Game.Utils;
using UnityEngine;

namespace Client.Game.Network
{
    public class ClientTcpMessengerServiceTest : IMessengerService
    {
        private readonly IMessageHandler messageHandler;
        private CancellationTokenSource connectionSource;
        private TcpClient connection;
        
        public ClientTcpMessengerServiceTest(IMessageHandler messageHandler)
        {
            this.messageHandler = messageHandler;
        }

        public void Dispose()
        {
            Close();
        }
        
        public async UniTask<bool> ConnectAsync()
        {
            try
            {
                Close();
                connection = new TcpClient(Urls.ServerUrl, Urls.Port);
                await connection.ConnectAsync(Urls.ServerUrl, Urls.Port);
                connectionSource = new CancellationTokenSource();
                SharedLogger.Log($"[Client.{GetType().Name}] Connected to the server!");
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
                if (connection is not {Connected: true})
                    return;
                
                connection?.Close();
                connection?.Dispose();
                connection = null;
                SharedLogger.Log($"[Client.{GetType().Name}] Shutdown.");
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
            }
        }

        public async UniTask SendAsync(Route route, object data, CancellationToken token = default)
        {
            if (connection is not {Connected:true})
            {
                SharedLogger.Error(connection.ReflectionFormat());
                throw new NotConnectedException();
            }
            
            var message = new Message
            {
                Route = route,
                Data = data as string ?? JsonConvert.SerializeObject(data, SerializeExtensions.SerializeSettings)
            };
            
            var jsonData = JsonConvert.SerializeObject(message, SerializeExtensions.SerializeSettings);
            var messageBytes = Encoding.UTF8.GetBytes(jsonData);
            var lengthPrefix = BitConverter.GetBytes(messageBytes.Length);
            var totalMessageBytes = lengthPrefix.Union(messageBytes).ToArray();
            
            await using var stream = connection.GetStream();
            await stream.WriteAsync(totalMessageBytes, token);
            // await stream.FlushAsync(token);
        }

        private async UniTask ReceiveAsync(CancellationToken token)
        {
            while (Application.isPlaying)
            {
                token.ThrowIfCancellationRequested();
                await using var stream = connection.GetStream();
                var lengthBuffer = new byte[4];
                var lengthRead = await stream.ReadAsync(lengthBuffer, 0, lengthBuffer.Length, token);
                if (lengthRead == 0)
                {
                    await ReceiveDelayTask(token);
                    continue;
                }
                
                var responseLength = BitConverter.ToInt32(lengthBuffer, 0);
                if (responseLength < 0)
                {
                    SharedLogger.Error($"[Client.{GetType().Name}] Server sent length header less than zero");
                    continue;
                }
                
                var buffer = new byte[responseLength];
                if (await stream.ReadAsync(buffer, lengthRead, buffer.Length, token) > 0)
                {
                    try
                    {
                        var jsonData = Encoding.UTF8.GetString(buffer);
                        messageHandler.Handle(JsonConvert.DeserializeObject<Message>(jsonData, SerializeExtensions.GetDeserializeSettingsByType<Message>()));
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
    }
}