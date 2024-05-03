using Startup;
using Zenject;

namespace Infrastructure
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