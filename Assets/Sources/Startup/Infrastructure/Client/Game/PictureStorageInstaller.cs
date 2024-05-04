using Core.PictureStorage;
using Zenject;

namespace Startup.Infrastructure.Client.Core
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