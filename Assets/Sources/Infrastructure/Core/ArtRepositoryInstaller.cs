using Core.Network.ArtRepository;
using Zenject;

namespace Core.Network.Infrastructure
{
    public class ArtRepositoryInstaller : Installer<ArtRepositoryInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IArtRepository>()
                .To<ArtRepository.ArtRepository>()
                .AsSingle();
        }
    }
}