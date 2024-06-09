using Server.Application.Network;
using Server.Application.Sessions;
using Zenject;

namespace Startup.Infrastructure.Server
{
    public class ServerApplicationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<ServerMessageHandler>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<ServerTcpMessengerService>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<RuntimeSessionsRepository>()
                .AsSingle()
                .NonLazy();
                        
            Container
                .BindInterfacesTo<SessionFactory>()
                .AsSingle()
                .NonLazy();
        }
    }
}