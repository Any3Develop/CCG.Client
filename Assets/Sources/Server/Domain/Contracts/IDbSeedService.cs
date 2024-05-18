using System.Threading.Tasks;

namespace Server.Domain.Contracts
{
    public interface IDbSeedService
    {
        Task SeedAsync();
    }
}