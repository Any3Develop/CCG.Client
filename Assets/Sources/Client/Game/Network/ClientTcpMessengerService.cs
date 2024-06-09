using System;
using System.IO;
using System.Linq;
using System.Net;
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

namespace Client.Game.Network
{
    public class ClientTcpMessengerService : IMessengerService
    {
        private readonly IMessageHandler messageHandler;
        private CancellationTokenSource connectionSource;
        private TcpClient client;
        private Stream connection;
        
        public ClientTcpMessengerService(IMessageHandler messageHandler)
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
                client = new TcpClient();
                await client.ConnectAsync(IPAddress.Parse(Urls.ServerUrl), Urls.Port);
                connection = client.GetStream();
                connectionSource = new CancellationTokenSource();
                SharedLogger.Log($"[Client.{GetType().Name}] Connected to the server!");
                ReceiveAsync(connectionSource.Token).Forget();
                return true;
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
                Close();
                return false;
            }
        }

        public void Close()
        {
            try
            {
                if (client == null)
                    return;

                client?.Close();
                client?.Dispose();
                connection = null;
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
        }

        public async UniTask SendAsync(Route route, object data, CancellationToken token = default)
        {
            if (client is not {Connected:true} || connection == null)
                throw new NotConnectedException();
            
            var message = new Message
            {
                Route = route,
                Data = data as string ?? JsonConvert.SerializeObject(data, SerializeExtensions.SerializeSettings)
            };
            
            var jsonData = JsonConvert.SerializeObject(message, SerializeExtensions.SerializeSettings);
            var messageBytes = Encoding.UTF8.GetBytes(jsonData);
            var lengthPrefix = BitConverter.GetBytes(messageBytes.Length);
            var totalMessageBytes = lengthPrefix.Concat(messageBytes).ToArray();
            
            while (connection is {CanWrite: false})
                await NetworkDelayTask(500, token);
            
            if (connection is not {CanWrite: true})
            {
                SharedLogger.Error($"[Client.{GetType().Name}] Can't send a message because connection with Server is closed.");
                return;
            }
            
            await connection.WriteAsync(totalMessageBytes, token);
        }

        private async UniTask ReceiveAsync(CancellationToken token)
        {
            try
            {
                var lengthBuffer = new byte[4];
                while (!token.IsCancellationRequested && client is {Connected: true})
                {
                    await NetworkDelayTask(token: token);
                    if (!connection.CanRead)
                        continue;
                    
                    var lengthRead = await connection.ReadAsync(lengthBuffer, 0, lengthBuffer.Length, token);
                    if (lengthRead == 0)
                        continue;

                    var responseLength = BitConverter.ToInt32(lengthBuffer, 0);
                    if (responseLength <= 0)
                    {
                        SharedLogger.Error($"[Client.{GetType().Name}] Server sent incorrect header.");
                        continue;
                    }

                    var buffer = new byte[responseLength];
                    if (await connection.ReadAsync(buffer, 0, buffer.Length, token) <= 0)
                        continue;

                    var data = Encoding.UTF8.GetString(buffer);
                    var message = JsonConvert.DeserializeObject<Message>(data, SerializeExtensions.DeserializeSettings);
                    messageHandler.Handle(message);
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
                Close();
            }
        }
        
        private static UniTask NetworkDelayTask(int miliseconds = 1000, CancellationToken token = default)
        {
            return UniTask.Delay(miliseconds, DelayType.Realtime, PlayerLoopTiming.EarlyUpdate, token);
        }
    }
}