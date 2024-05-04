using CardGame.Cards;
using Zenject;

namespace Startup.Infrastructure.Client.Core
{
    public class CardsInstaller : Installer<CardsInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<CardSceneObjectStorage>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<CardModelStorage>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<CardDtoStorage>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<CardStatModelStorage>()
                .AsSingle();
        }
    }
}