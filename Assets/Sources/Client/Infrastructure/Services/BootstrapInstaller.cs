using CardGame.Services.BootstrapService;
using Zenject;

namespace Infrastructure
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