using System.Threading.Tasks;
using Server.Domain.Contracts.Persistence;

namespace Server.Infrastructure.Persistence
{
    public class TestDbSeedService : IDbSeedService
    {
        public Task SeedAsync()
        {
            return Task.CompletedTask;
        }
    }
}