using Startup.Infrastructure.Client.Common;
using Startup.Infrastructure.Client.Common.Services;
using Startup.Infrastructure.Client.Core;
using Zenject;

namespace Startup.Infrastructure
{
    public class StartupInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            RenderingInstaller.Install(Container);
            SceneEntityInstaller.Install(Container);
            ArtRepositoryInstaller.Install(Container);
            PictureStorageInstaller.Install(Container);
            SessionsInstaller.Install(Container);
            CardsInstaller.Install(Container);
        }

        public override void Start()
        {
            base.Start();
            Container.Instantiate<StartupApplication>().Initialize();
        }
    }
}