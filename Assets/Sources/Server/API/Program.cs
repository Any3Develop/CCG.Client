using System;
using System.Threading.Tasks;
using Server.Domain.Contracts.Messanger;
using Server.Domain.Contracts.Persistence;

namespace Server.API
{
    public class Program : IDisposable
    {
        private readonly IDbSeedService dbSeedService;
        private readonly IMessengerService messengerService;
        
        public Program(IDbSeedService dbSeedService, IMessengerService messengerService)
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