using CardGame.App.Loader;
using CardGame.Cameras;
using CardGame.Cards;
using CardGame.ImageRepository;
using CardGame.Services.SceneEntity;
using CardGame.Session;
using Zenject;

namespace CardGame.App
{
    public class CardGameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            CamerasInstaller.Install(Container);
            SceneEntityInstaller.Install(Container);
            ImageRepositoryInstaller.Install(Container);
            SessionsInstaller.Install(Container);
            CardsInstaller.Install(Container);
        }

        public override void Start()
        {
            base.Start();
            Container.Instantiate<CardGameLoader>().Initialize();
        }
    }
}