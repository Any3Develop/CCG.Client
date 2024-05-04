using CardGame.Services.BootstrapService;
using Zenject;

namespace Startup.Infrastructure.Client.Common.Services
{
    public class BootstrapInstaller : Installer<BootstrapInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IBootstrap>()
                .To<Bootstrap>()
                .AsTransient();
        }
    }
}