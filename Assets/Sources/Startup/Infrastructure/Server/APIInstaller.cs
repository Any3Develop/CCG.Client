using Server.API;
using Zenject;

namespace Startup.Infrastructure.Server
{
    public class APIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<Program>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<ApiLauncher>()
                .AsSingle()
                .NonLazy();
        }
    }
}