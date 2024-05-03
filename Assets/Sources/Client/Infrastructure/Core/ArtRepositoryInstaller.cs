using Core.ArtRepository;
using Zenject;

namespace Infrastructure
{
    public class ArtRepositoryInstaller : Installer<ArtRepositoryInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IArtRepository>()
                .To<ArtRepository>()
                .AsSingle();
        }
    }
}