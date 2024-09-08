using System.Threading;
using System.Threading.Tasks;
using Server.Application.Contracts.Network;
using Server.Domain.Contracts;
using Server.Domain.Contracts.Persistence;

namespace Server.API
{
    public class Program : IProgram
    {
        private readonly IDbSeedService dbSeedService;
        private readonly INetworkHubConnection networkHubConnection;
        private readonly CancellationTokenSource lifeTime;
        
        public Program(
            IDbSeedService dbSeedService,
            INetworkHubConnectionFactory hubConnectionFactory)
        {
            this.dbSeedService = dbSeedService;
            networkHubConnection = hubConnectionFactory.Create();
            lifeTime = new CancellationTokenSource();
        }

        public async Task Main()
        {
            await dbSeedService.SeedAsync();
            await networkHubConnection
                .ConnectAsync(Urls.Port, lifeTime.Token)
                .ConfigureAwait(false);
        }

        public void Dispose()
        {
            networkHubConnection.CloseAsync()
                .ConfigureAwait(false)
                .GetAwaiter();
        }
    }
}