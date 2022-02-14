using Zenject;

namespace CardGame.Services.StateMachine
{
    public class StateMachineInstaller : Installer<StateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IStateMachine>()
                .To<StateMachine>()
                .AsTransient();
        }
    }
}