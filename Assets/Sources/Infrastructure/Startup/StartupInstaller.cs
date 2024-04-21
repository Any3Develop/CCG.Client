using CardGame.Cameras;
using CardGame.Cards;
using CardGame.ImageRepository;
using CardGame.Services.SceneEntity;
using CardGame.Session;
using Core.Network.Infrastructure;
using Startup;
using Zenject;

namespace CardGame.App
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