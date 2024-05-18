using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Server.Domain.Contracts;
using Server.WebAPI.Network;
using Shared.Common.Logger;

namespace Server.WebAPI
{
    public class Program : IDisposable
    {
        private readonly IDbSeedService dbSeedService;
        private const int Port = 80;
        private TcpListener listener;
        
        public Program(IDbSeedService dbSeedService)
        {
            this.dbSeedService = dbSeedService;
        }

        public void Main()
        {
            dbSeedService.SeedAsync().ContinueWith(() =>
            {
                listener = new TcpListener(IPAddress.Any, Urls.Port);
                listener.Start();
                SharedLogger.Log($"Server started on port {Port}");
                AcceptClientsAsyncLoop().GetAwaiter();
            });
        }

        private async Task AcceptClientsAsyncLoop()
        {
            var acceptDelay = TimeSpan.FromSeconds(1);
            while (listener != null)
            {
                var client = await listener.AcceptTcpClientAsync();
                if (client != null)
                    SharedLogger.Log($"Client connected : {client}");

                await Task.Delay(acceptDelay);
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            
        }

        public void Dispose()
        {
            listener?.Stop();
            listener = null;
        }
    }
}