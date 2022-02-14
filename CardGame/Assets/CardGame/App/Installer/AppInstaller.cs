using CardGame.Services.BootstrapService;
using CardGame.Services.InputService;
using CardGame.Services.MonoPoolService;
using CardGame.Services.StateMachine;
using CardGame.Services.StatsService;
using CardGame.Services.TypeRegistryService;
using CardGame.Services.UIService;
using Zenject;

namespace CardGame.App
{
    public class AppInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InputSystemInstaller.Install(Container);
            TypeRegistryInstaller.Install(Container);
            StatsServiceInstaller.Install(Container);
            StateMachineInstaller.Install(Container);
            BootstrapInstaller.Install(Container);
            MonoPoolInstaller.Install(Container);
            UIServiceInstaller.Install(Container);
        }
    }
}