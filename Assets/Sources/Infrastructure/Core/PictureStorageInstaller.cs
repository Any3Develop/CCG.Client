using Zenject;

namespace CardGame.ImageRepository
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