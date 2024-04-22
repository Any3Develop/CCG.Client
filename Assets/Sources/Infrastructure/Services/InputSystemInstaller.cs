using CardGame.Services.InputService;
using Zenject;

namespace Infrastructure
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