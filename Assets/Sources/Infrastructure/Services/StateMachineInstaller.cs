using CardGame.Services.StateMachine;
using Zenject;

namespace Infrastructure
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