using CardGame.Cards;
using Zenject;

namespace Infrastructure
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