using Server.Application.Messenger;
using Server.Domain.Contracts;
using Server.Infrastructure.Persistence;
using Zenject;

namespace Startup.Infrastructure.Server
{
    public class ServerTestFlyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<TestDbSeedService>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<TestMessengerHandler>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<ServerTcpMessengerService>()
                .AsSingle()
                .NonLazy();
        }
        
        public override void Start()
        {
            base.Start();
            var program = Container.Resolve<IProgram>();
            program.Main().GetAwaiter();
        }
    }
}