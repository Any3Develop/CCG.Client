using Zenject;

namespace CardGame.Services.InputService
{
    public class InputSystemInstaller : Installer<InputSystemInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<InputController<UIInputLayer>>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<InputController<HandFieldLayer>>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<InputController<TableFieldLayer>>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<MainInputController>()
                .AsSingle();
        }
    }
}