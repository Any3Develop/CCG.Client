using Zenject;

namespace CardGame.Services.StatsService
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
