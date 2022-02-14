using Zenject;

namespace CardGame.Services.BootstrapService
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