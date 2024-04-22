using Core.PictureStorage;
using Zenject;

namespace Infrastructure
{
    public class PictureStorageInstaller : Installer<PictureStorageInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<PictureStorage>()
                .AsSingle();
        }
    }
}