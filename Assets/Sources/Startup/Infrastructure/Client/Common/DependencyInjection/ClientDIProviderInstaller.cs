using Zenject;

namespace Startup.Infrastructure.Client.Common.DependencyInjection
{
    public class ClientDiProviderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<ZenjectDiProvider>()
                .AsTransient();
        }
    }
}