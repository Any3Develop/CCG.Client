using System.Threading.Tasks;

namespace Server.Domain.Contracts.Persistence
{
    public interface IDbSeedService
    {
        Task SeedAsync();
    }
}