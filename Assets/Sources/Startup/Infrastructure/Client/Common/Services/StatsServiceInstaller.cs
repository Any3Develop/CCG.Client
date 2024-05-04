using CardGame.Services.StatsService;
using Zenject;

namespace Startup.Infrastructure.Client.Common.Services
{
    public class StatsServiceInstaller : Installer<StatsServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<StatsCollectionStorage>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<StatsCollectionDtoStorage>()
                .AsSingle()
                .NonLazy();
        }
    }
}
