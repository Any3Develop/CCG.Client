using Zenject;

namespace CardGame.ImageRepository
{
    public class ImageRepositoryInstaller : Installer<ImageRepositoryInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IImageRepository>()
                .To<ImageRepository>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<ImageStorage>()
                .AsSingle();
        }
    }
}