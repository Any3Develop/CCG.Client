using Server.Infrastructure.Persistence;
using Zenject;

namespace Startup.Infrastructure.Server
{
    public class ServerInfrastructureInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<AppDbContext>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<DbSeedService>()
                .AsSingle()
                .NonLazy();
        }
    }
}