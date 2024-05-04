using CardGame.Services.StateMachine;
using Zenject;

namespace Startup.Infrastructure.Client.Common.Services
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