using Core.ArtRepository;
using Zenject;

namespace Startup.Infrastructure.Client.Core
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