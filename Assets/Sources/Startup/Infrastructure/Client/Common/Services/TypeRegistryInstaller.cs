using CardGame.Services.TypeRegistryService;
using Zenject;

namespace Startup.Infrastructure.Client.Common.Services
{
    public class TypeRegistryInstaller : Installer<TypeRegistryInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<TypeRegistry>()
                .AsSingle()
                .NonLazy();
        }
    }
}