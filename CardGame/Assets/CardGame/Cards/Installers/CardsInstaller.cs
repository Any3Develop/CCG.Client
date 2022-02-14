using Zenject;

namespace CardGame.Cards
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