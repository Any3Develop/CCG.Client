using System.Threading.Tasks;
using Server.Application.Contracts.Network;
using Server.Domain.Contracts;
using Server.Domain.Contracts.Persistence;

namespace Server.API
{
    public class Program : IProgram
    {
        private readonly IDbSeedService dbSeedService;
        private readonly IMessengerService messengerService;
        
        public Program(
            IDbSeedService dbSeedService, 
            IMessengerService messengerService)
        {
            this.dbSeedService = dbSeedService;
            this.messengerService = messengerService;
        }

        public async Task Main()
        {
            await dbSeedService.SeedAsync();
            messengerService.Start(Urls.Port);
        }

        public void Dispose()
        {
            messengerService?.Stop();
        }
    }
}