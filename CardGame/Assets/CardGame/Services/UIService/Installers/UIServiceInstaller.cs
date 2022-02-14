using Zenject;

namespace CardGame.Services.UIService
{
    public class UIServiceInstaller : Installer<UIServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<UIRoot>()
                .FromComponentInNewPrefabResource("UIRoot")
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<IUIService>()
                .To<UIService>()
                .AsSingle()
                .NonLazy();
        }
    }
}