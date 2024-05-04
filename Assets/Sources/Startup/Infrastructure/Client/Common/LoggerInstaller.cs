using Client.Common.Logger;
using Zenject;

namespace Startup.Infrastructure.Client.Common
{
    public class LoggerInstaller : Installer<LoggerInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<ClientSharedLogger>()
                .AsSingle()
                .NonLazy();
        }
    }
}