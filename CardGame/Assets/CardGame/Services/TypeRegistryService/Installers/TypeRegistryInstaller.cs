using Zenject;

namespace CardGame.Services.TypeRegistryService
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