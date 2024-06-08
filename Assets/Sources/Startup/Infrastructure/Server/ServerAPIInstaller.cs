using Server.API;
using Zenject;

namespace Startup.Infrastructure.Server
{
    public class ServerAPIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<Program>()
                .AsSingle()
                .NonLazy();
        }
    }
}