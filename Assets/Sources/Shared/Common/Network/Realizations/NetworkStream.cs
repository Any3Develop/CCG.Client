using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shared.Abstractions.Common.Network;
using Shared.Common.Network.Data;

namespace Shared.Common.Network
{
    public class NetworkStream : INetworkStream
    {
        private readonly Stream stream;
        private readonly byte[] lengthBuffer = {0,0,0,0};
        private static readonly StreamResult Empty = default;
        
        public NetworkStream(Stream stream)
        {
            this.stream = stream;
        }
        
        public async Task<StreamResult> ReadAsync(CancellationToken token)
        {
            if (stream is not {CanRead: true})
                return Empty;
            
            var lengthRead = await stream.ReadAsync(lengthBuffer, 0, lengthBuffer.Length, token);
            if (lengthRead == 0)
                return Empty;

            var responseLength = BitConverter.ToInt32(lengthBuffer, 0);
            for (var i = 0; i < lengthBuffer.Length; i++) // clear lengthBuffer
                lengthBuffer[i] = 0;
            
            if (responseLength <= 0)
                throw new InvalidOperationException($"[{GetType().Name}] Received incorrect header.");
            
            var buffer = new byte[responseLength];
            if (await stream.ReadAsync(buffer, 0, buffer.Length, token) <= 0)
                return Empty;

            return new StreamResult(buffer);
        }

        public async Task<StreamResult> WriteAsync(byte[] buffer, CancellationToken token)
        {
            if (buffer is null or {Length: 0})
                return Empty;

            var tryCounter = 3;
            while (stream is {CanWrite: false} && tryCounter > 0)
            {
                tryCounter--;
                await Task.Delay(500, token); // wait while can't read.
            }

            if (stream is not {CanWrite: true})
                throw new Exception($"[Client.{GetType().Name}] Can't send a message because connection with Server is not reachable.");

            var lengthPrefix = BitConverter.GetBytes(buffer.Length);
            buffer = lengthPrefix.Concat(buffer).ToArray();
                
            await stream.WriteAsync(buffer, token);
            return new StreamResult(buffer);
        }
    }
}