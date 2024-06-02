using Shared.Game.Context;
using Shared.Game.Factories;
using Zenject;

namespace Startup.Infrastructure.Server
{
    public class ServerSharedInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<Database>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<SharedConfig>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<ContextFactory>()
                .AsSingle()
                .NonLazy();
        }
    }
}