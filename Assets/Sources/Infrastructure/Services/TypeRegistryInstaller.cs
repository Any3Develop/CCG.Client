using CardGame.Services.TypeRegistryService;
using Zenject;

namespace Infrastructure
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